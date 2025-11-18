# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

**minfraud-api-dotnet** is MaxMind's official .NET client library for the minFraud fraud detection web services:
- **minFraud Score**: Risk score for transactions
- **minFraud Insights**: Score plus GeoIP2 data and device/email intelligence
- **minFraud Factors**: All Insights data plus risk reasons and deprecated subscores
- **Transaction Reporting API**: Report chargebacks and fraud to improve minFraud accuracy

The library provides both synchronous and asynchronous methods, supports ASP.NET Core dependency injection, and integrates deeply with MaxMind's GeoIP2 library for IP intelligence.

**Key Technologies:**
- .NET 10.0, .NET 9.0, .NET 8.0, .NET Standard 2.1, and .NET Standard 2.0
- System.Text.Json for JSON serialization/deserialization
- MaxMind.GeoIP2 library (critical dependency for response models)
- xUnit for testing
- Modern C# features (nullable reference types, init-only properties, C# 14.0)

## Development Commands

### Building
```bash
# Build main library
dotnet build MaxMind.MinFraud

# Build tests
dotnet build MaxMind.MinFraud.UnitTest

# Build everything
dotnet build MaxMind.MinFraud.sln
```

### Testing
```bash
# Run all tests
dotnet test MaxMind.MinFraud.UnitTest/MaxMind.MinFraud.UnitTest.csproj

# Run specific test class
dotnet test --filter "FullyQualifiedName~WebServiceClientTest"

# Run tests with detailed output
dotnet test -v normal
```

### Other Commands
```bash
# Check for outdated dependencies
dotnet-outdated

# Package for NuGet (requires dev-bin/release.ps1 PowerShell script)
# See README.dev.md for full release process
```

## Code Architecture

### Project Structure

```
MaxMind.MinFraud/
├── Exception/          # 6 exception types (MinFraudException hierarchy)
├── Request/            # Request models (Transaction, TransactionReport, etc.)
│   ├── Transaction.cs         # Main request aggregator (11 optional sub-models)
│   ├── TransactionReport.cs   # Fraud/chargeback reporting
│   ├── Device.cs, Account.cs, Email.cs, etc.
│   └── CustomInputs.cs        # Builder pattern for custom fields
├── Response/           # Response models (Score, Insights, Factors)
│   ├── Score.cs               # Base response (RiskScore, Id, Warnings)
│   ├── Insights.cs            # Extends Score, adds GeoIP2 & device data
│   ├── Factors.cs             # Extends Insights, adds RiskScoreReasons
│   └── IPAddress.cs, Device.cs, Email.cs, etc.
├── Util/               # JSON converters and helpers
├── WebServiceClient.cs        # Main API client
└── IWebServiceClient.cs       # Interface for DI/mocking

MaxMind.MinFraud.UnitTest/
├── Request/, Response/, Exception/ # Mirror structure
├── TestData/           # JSON fixtures for testing
└── WebServiceClientTest.cs
```

### Key Design Patterns

#### 1. **Request Model Composition with Init-Only Properties**

The `Transaction` model aggregates 11 optional sub-models using init-only properties:

```csharp
var transaction = new Transaction {
    Device = new Device { IPAddress = IPAddress.Parse("1.2.3.4") },
    Account = new Account { UserId = "user123" },
    Email = new Email { Address = "user@example.com" },
    // ... 8 more optional components
};
```

**Key Points:**
- All request properties use `init` setters (immutable after construction)
- Validation happens immediately in property setters (fail-fast)
- JSON serialization uses `[JsonPropertyName("snake_case")]` attributes
- Auto-hashing for usernames/addresses (MD5) when configured

#### 2. **Response Hierarchy: Score → Insights → Factors**

```
Score (base)
  ├─ Properties: RiskScore, Id, Disposition, Warnings, FundsRemaining, IPAddress
  └─ Insights (extends Score)
      ├─ Overrides IPAddress with full GeoIP2 data
      ├─ Adds: CreditCard, Device, Email, BillingAddress, ShippingAddress, Phones
      └─ Factors (extends Insights)
          └─ Adds: RiskScoreReasons, Subscores (obsolete as of 2025-03)
```

**Key Points:**
- Each level adds more detail without breaking compatibility
- Interface-based polymorphism for `IIPAddress` (Score uses minimal, Insights uses full)
- Response models inherit from GeoIP2 models (e.g., `IPAddress : InsightsResponse`)
- All response properties are init-only with default empty objects (never null)

#### 3. **GeoIP2 Integration via Inheritance**

minFraud response models inherit from and extend GeoIP2 models:

```csharp
public sealed class IPAddress : InsightsResponse, IIPAddress
{
    // Inherits: City, Country, Location, Postal, RegisteredCountry, RepresentedCountry, Traits
    // Adds minFraud-specific:
    public double? Risk { get; init; }
    public IReadOnlyList<IPRiskReason> RiskReasons { get; init; }
}
```

**Implications:**
- Changes to GeoIP2 models affect minFraud responses
- When adding fields, check if they belong in GeoIP2 or minFraud layer
- Use GeoIP2 patterns for location-related data

#### 4. **Email Address Normalization**

The `Email` class performs sophisticated normalization when `HashAddress = true`:
- Domain typo fixes: `gmai.com` → `gmail.com`
- TLD typo fixes: `example.comm` → `example.com`
- Equivalent domains: `googlemail.com` → `gmail.com`
- Plus/dash alias removal for common providers
- Gmail period removal: `f.o.o@gmail.com` → `foo@gmail.com`
- Fastmail subdomain normalization
- NFC normalization of local parts

**Location:** `MaxMind.MinFraud/Request/Email.cs`

#### 5. **CustomInputs Builder Pattern**

For user-defined fields (up to 25 keys, max 255 characters per value):

```csharp
var customInputs = new CustomInputs.Builder {
    { "float_input", 12.1d },
    { "integer_input", 3123 },
    { "string_input", "value" },
    { "boolean_input", true }
}.Build();
```

**Key Points:**
- One-time use (builder invalidated after `Build()`)
- Type-safe overloads for supported types
- Validation: key format `^[a-z0-9_]{1,25}$`, numeric range ±10^13
- Uses `[JsonExtensionData]` for dynamic serialization

#### 6. **Thread-Safe WebServiceClient**

```csharp
using var client = new WebServiceClient(accountId: 10, licenseKey: "KEY");

// Three query methods (all async):
var score = await client.ScoreAsync(transaction);
var insights = await client.InsightsAsync(transaction);
var factors = await client.FactorsAsync(transaction);

// Reporting method:
await client.ReportAsync(report);
```

**Key Points:**
- Thread-safe, designed for singleton/reuse across requests
- Implements `IDisposable` for proper HttpClient lifecycle
- Base path: `https://minfraud.maxmind.com/minfraud/v2.0/{endpoint}`
- Sandbox support via `host: "sandbox.maxmind.com"` parameter
- Custom User-Agent: `"minFraud-api-dotnet/{version}"`

#### 7. **ASP.NET Core Integration**

```csharp
// Startup.cs
services.Configure<WebServiceClientOptions>(Configuration.GetSection("MinFraud"));
services.AddHttpClient<WebServiceClient>();

// appsettings.json
{
  "MinFraud": {
    "AccountId": 10,
    "LicenseKey": "LICENSEKEY",
    "Timeout": "00:00:05", // optional
    "Host": "minfraud.maxmind.com" // optional
  }
}

// Controller
public MyController(WebServiceClient client) { ... }
```

## Testing Conventions

### Test Structure
- Tests use xUnit framework
- HTTP mocking via RichardSzalay.MockHttp
- JSON fixtures in `TestData/` for response deserialization tests
- Test structure mirrors main project structure

### Test Patterns

**1. HTTP Mocking:**
```csharp
var mockHttp = new MockHttpMessageHandler();
mockHttp.When(HttpMethod.Post, "https://minfraud.maxmind.com/minfraud/v2.0/score")
    .Respond(HttpStatusCode.OK, "application/json", responseContent);

var client = new WebServiceClient(new HttpClient(mockHttp), options);
```

**2. Validation Testing:**
```csharp
[Theory]
[InlineData(-1.0)]  // Invalid
[InlineData(3600.1)] // Valid
public void TestSessionAge(double age) { ... }
```

**3. JSON Round-Trip Testing:**
Test that response objects can be serialized back to JSON and match the original structure.

## Working with This Codebase

### Adding New Fields to Existing Request Models

1. **Add property** with JSON attribute and validation:
   ```csharp
   private readonly double? _fieldName;

   [JsonPropertyName("field_name")]
   public double? FieldName {
       get => _fieldName;
       init {
           if (value is < 0) {
               throw new ArgumentException("must be non-negative.");
           }
           _fieldName = value;
       }
   }
   ```

2. **Update `releasenotes.md`** with the change

3. **Add tests** for validation and serialization

### Adding New Fields to Existing Response Models

1. **Determine if field is GeoIP2 or minFraud-specific**
   - GeoIP2: Location data, ISP, traits, etc. → Add to GeoIP2 library first
   - minFraud: Risk, device intelligence, email intelligence → Add here

2. **Add property** with init-only setter:
   ```csharp
   [JsonInclude]
   [JsonPropertyName("field_name")]
   public TypeName? FieldName { get; init; }
   ```

3. **For MINOR version releases**: Add deprecated constructor matching old signature to avoid breaking changes:
   ```csharp
   // New constructor with added parameter
   public ResponseClass(
       string? existingField = null,
       double? newField = null  // NEW
   ) { ... }

   // Deprecated constructor for backward compatibility
   [Obsolete("Use constructor with newField parameter")]
   public ResponseClass(string? existingField = null)
       : this(existingField, null) { }
   ```

4. **For MAJOR versions**: No need for deprecated constructor

5. **Update tests** with assertions for the new field

6. **Update `releasenotes.md`**

### Adding New Enum Values

When MaxMind adds new payment processors, event types, etc.:

1. **Add to enum** (e.g., `PaymentProcessor`, `EventType`):
   ```csharp
   [EnumMember(Value = "new_processor")]
   NewProcessor,
   ```

2. **Update `releasenotes.md`** listing the new value

3. **No tests needed** for simple enum additions

### Exception Handling Strategy

**Exception Hierarchy:**
```
System.Exception
  ├─ IOException
  │   └─ HttpException (transport errors, 5xx)
  └─ MinFraudException (service errors)
      ├─ AuthenticationException (401, invalid credentials)
      ├─ InsufficientFundsException (payment required)
      ├─ PermissionRequiredException (403, service not enabled)
      └─ InvalidRequestException (400, bad request)
```

**Error Handling:**
- HTTP-level errors (network, 500s) → `HttpException` (inherits from `IOException`)
- Service errors (4xx) → Parse JSON error body → Specific `MinFraudException` subclass
- Validation errors (construction) → `ArgumentException` (fail-fast)
- Non-fatal issues → `Score.Warnings` list (not thrown)

### releasenotes.md Format

Always update `releasenotes.md` for user-facing changes:

```markdown
5.x.0 (YYYY-MM-DD)
------------------

* Added `NewProperty` property to `MaxMind.MinFraud.Response.ResponseClass`.
  This provides information about...
* Added `NewValue` to the `EnumName` enum.
* The `OldProperty` property in `MaxMind.MinFraud.Model.ModelClass` has been
  marked `Obsolete`. Please use `NewProperty` instead.
```

For MAJOR versions, prefix breaking changes with `**BREAKING:**`

### Deprecation Guidelines

1. **Use `[Obsolete]` attribute** with helpful messages:
   ```csharp
   [Obsolete("Use NewProperty instead. This will be removed in v6.0.0.")]
   public string? OldProperty { get; init; }
   ```

2. **Keep deprecated functionality working** through the minor version series

3. **Update `releasenotes.md`** with deprecation notice

4. **Remove in next MAJOR version** (not minor/patch)

### Code Quality Standards

The project enforces strict standards:
- **EnforceCodeStyleInBuild**: Code style violations are build errors
- **TreatWarningsAsErrors**: All warnings must be resolved
- **EnableNETAnalyzers**: .NET code analyzers enabled
- **.editorconfig**: Defines consistent coding style
- **Language Version**: C# 13.0
- **Nullable Reference Types**: Enabled (`<Nullable>enable</Nullable>`)

### Multi-Framework Considerations

This library targets multiple frameworks. When adding features:

1. **Prefer standard types** that work across all targets
2. **For newer types** (e.g., `DateOnly` in .NET 6+), use conditional compilation:
   ```csharp
   #if NET6_0_OR_GREATER
       public DateOnly? SomeDate { get; init; }
   #endif
   ```
3. **Test on both modern .NET and .NET Standard** if possible

## Common Patterns and Conventions

### Pattern: Immutability
- All request/response models use init-only properties
- No public setters after construction
- Defensive copying (readonly collections, immutable types)
- Use `IReadOnlyList<T>` for collections in responses

### Pattern: Validation in Property Setters
```csharp
private readonly double? _value;

public double? Value {
    get => _value;
    init {
        if (value is < 0 or > 100) {
            throw new ArgumentException("must be between 0 and 100.");
        }
        _value = value;
    }
}
```

### Pattern: Default Empty Objects (Never Null)
```csharp
public CreditCard CreditCard { get; init; } = new();
public IReadOnlyList<Warning> Warnings { get; init; } = [];
```

This prevents null reference exceptions and simplifies client code.

### Pattern: Computed Properties (Not Serialized)
```csharp
[JsonIgnore]
public string? Username { get; init; }

[JsonPropertyName("username_md5")]
public string? UsernameMD5 {
    get {
        if (Username == null) return null;
        // ... compute MD5
    }
}
```

### Pattern: EnumMember for JSON Serialization
```csharp
public enum EventType
{
    [EnumMember(Value = "account_creation")]
    AccountCreation,

    [EnumMember(Value = "account_login")]
    AccountLogin,
}
```

Requires `EnumMemberValueConverter<T>` in JSON options.

## Dependencies

### Critical Dependencies

**MaxMind.GeoIP2**
- Provides base models for response classes (InsightsResponse, Location, Traits, etc.)
- minFraud responses inherit from and extend these models
- Changes to GeoIP2 models can affect minFraud API surface

**System.Text.Json**
- Modern JSON serialization (not Newtonsoft.Json)
- Requires custom converters for enums, IP addresses, GeoIP2 types
- Uses snake_case naming via `[JsonPropertyName]` attributes

**Microsoft.Extensions.Options**
- ASP.NET Core configuration binding
- Enables dependency injection pattern

**IsExternalInit** (.NET Standard only)
- Polyfill for C# 9.0 `init` keyword on older frameworks

## Version Requirements

- **Target Frameworks**: net10.0, net9.0, net8.0, netstandard2.1, netstandard2.0
- **Language Version**: C# 14.0
- **Assembly Signing**: Uses MaxMind.snk strong name key
- **NuGet Package**: MaxMind.MinFraud

## Additional Resources

- [API Documentation](https://maxmind.github.io/minfraud-api-dotnet/)
- [minFraud Web Services Docs](https://dev.maxmind.com/minfraud?lang=en)
- [minFraud Response Docs](https://dev.maxmind.com/minfraud/api-documentation/responses/)
- [GitHub Issues](https://github.com/maxmind/minfraud-api-dotnet/issues)
- [Release Process](README.dev.md)

---

*Last Updated: 2025-11-18*
