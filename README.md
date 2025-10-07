# GeeksCoreLibrary - Mollie

## Repository Metadata
- **Type**: Library / Integration Module
- **Language(s)**: C# (.NET 9.0)
- **Framework(s)**: .NET 9.0, ASP.NET Core
- **Database**: MySQL (via GeeksCoreLibrary)
- **Status**: Production
- **Critical Level**: High
- **Last Updated**: October 2025

## Purpose & Context

### What This Repository Does
The GeeksCoreLibrary Mollie module is a payment service provider integration that enables Mollie payment processing within the Wiser CMS e-commerce ecosystem. It provides a complete implementation of the Mollie Orders API, handling payment requests, status updates, webhooks, and payment returns for online orders processed through the Wiser platform.

### Business Value
This module enables Wiser-powered e-commerce sites to accept payments through Mollie, one of Europe's leading payment service providers. It supports multiple payment methods including iDEAL, credit cards, bank transfers, and other payment options available through Mollie's platform.

Key business benefits:
- Enables secure online payment processing for Wiser e-commerce sites
- Supports multiple payment methods through a single integration
- Handles complex order scenarios with multiple shopping baskets
- Provides comprehensive payment logging and audit trails
- Seamlessly integrates with Wiser's order processing workflow

### Wiser Ecosystem Role
This module is a plugin for the GeeksCoreLibrary's order process system. It:
- Extends the core payment processing capabilities of GeeksCoreLibrary
- Implements the `IPaymentServiceProviderService` interface
- Integrates with the shopping basket service for order line conversion
- Utilizes Wiser's item/detail storage model for order and user data
- Published as a NuGet package for easy inclusion in Wiser installations

## Core Functionality

### Main Components

**MollieService** - Main payment integration service implementing:

1. **`HandlePaymentRequestAsync()`**
   - Initiates a payment transaction with Mollie
   - Converts shopping baskets to Mollie order format
   - Returns checkout URL for customer redirect
   - Stores payment ID and status in shopping basket

2. **`ProcessStatusUpdateAsync()`**
   - Handles webhook callbacks from Mollie when payment status changes
   - Retrieves order details from Mollie API
   - Logs incoming payment actions
   - Returns success if status equals "paid"

3. **`HandlePaymentReturnAsync()`**
   - Handles user return from Mollie checkout page
   - Loads shopping baskets by invoice number
   - Fetches current order status from Mollie
   - Determines redirect URL based on status (success/pending/fail)

4. **`GetProviderSettingsAsync()`**
   - Loads Mollie-specific settings from database
   - Determines environment (dev/test vs production)
   - Decrypts and loads appropriate API key

### Payment Method Support

**iDEAL (Netherlands):**
- Special handling for Dutch bank selection
- Supports all major Dutch banks (ABN AMRO, ING, Rabobank, etc.)
- Direct payment without showing bank selection on Mollie page

**Other Methods:**
- Credit cards (Visa, Mastercard, American Express)
- Bank transfers
- PayPal, Apple Pay
- Local payment methods (Bancontact, Sofort, etc.)

### Key Business Rules

**Price and VAT Calculation:**
- Supports complex European VAT calculations
- Handles reduced rates, zero rates, special rates
- Precise financial data for each order line (unit price, VAT, discounts)

**Phone Number Formatting:**
- Converts phone numbers to E.164 international format
- Required by Mollie for all orders
- Uses libphonenumber-csharp library

**Error Handling and Logging:**
- Comprehensive logging of all Mollie interactions
- Logs payment requests, webhooks, and responses
- Extracts user-friendly error messages from Mollie API

## Installation & Setup

### Prerequisites
- **.NET 9.0 SDK**
- **GeeksCoreLibrary v5.3.2508.1+**
- **Mollie Account** with API credentials
- **MySQL Database**
- **Wiser CMS Installation**

### Configuration

Configuration stored in Wiser database as `payment_service_provider` items:

**Required Settings:**
- `mollieapikeylive` - Production API key (encrypted)
- `mollieapikeytest` - Test/development API key (encrypted)
- Return URL, Webhook URL, Success URL, Fail URL, Pending URL

**Optional System Objects:**
- `MOLLIE_locale` - Force specific locale (e.g., "en_US", "nl_NL")

## Troubleshooting

### Common Issues

**"Invalid API key" Error:**
- Verify API key exists in database
- Check environment configuration matches key type
- Test API key directly with Mollie API

**Webhook Not Receiving Updates:**
- Verify webhook URL is publicly accessible
- Check Mollie dashboard for delivery attempts
- Ensure endpoint returns HTTP 200

**Phone Number Validation Fails:**
- Ensure country code is present and valid
- Check phone number format
- Consider adding fallback for invalid numbers

**VAT Calculation Errors:**
- Verify VAT calculation logic
- Ensure all prices formatted to 2 decimals
- Use decimal type (not double/float)

## Known Issues & Technical Debt

- [ ] **Error Log References Wrong Provider** - Some logs say "Dimoco" instead of "Mollie"
- [ ] **Limited Webhook Security** - No verification that requests come from Mollie
- [ ] **No Request Rate Limiting** - Webhook endpoint could be abused
- [ ] **Minimal Unit Test Coverage** - Need comprehensive tests

## External Resources

- [Mollie Orders API Documentation](https://docs.mollie.com/reference/v2/orders-api/overview)
- [Mollie Dashboard](https://www.mollie.com/dashboard)
- [Mollie Status Page](https://status.mollie.com/)

---

**License**: GNU General Public License v3.0
**Package**: [GeeksCoreLibrary.Modules.Payments.Mollie on NuGet](https://www.nuget.org/packages/GeeksCoreLibrary.Modules.Payments.Mollie/)

*This documentation is part of the Wiser Platform knowledge base maintained by Happy Horizon B.V.*