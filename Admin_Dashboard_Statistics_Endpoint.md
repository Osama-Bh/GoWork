# Admin Dashboard Statistics API Documentation

## Overview

This endpoint returns the summary statistics needed for the shared `لوحة التحكم` page used by both `Admin` and `SubAdmin`.

It is designed specifically for the top dashboard cards and should be used as a single source for the page summary numbers.

## Endpoint

**Method:** `GET`  
**URL:** `/api/Admin/dashboard-statistics`

## Authorization

This endpoint is intended for authenticated dashboard users with one of these roles:

- `Admin`
- `SubAdmin`

## Query Parameters

This endpoint does not accept any query parameters.

## Response Shape

```json
{
  "statusCode": 200,
  "data": {
    "totalCompanies": 67,
    "totalFeedbacks": 20,
    "pendingVerificationRequests": 42,
    "unreadFeedbacks": 6,
    "totalPublishedJobs": 134,
    "companiesRegisteredThisMonth": 9,
    "newFeedbacksThisWeek": 4
  },
  "message": null
}
```

## Response Fields

| Field | Type | Description |
|---|---|---|
| `totalCompanies` | int | Total number of companies registered in the platform. |
| `totalFeedbacks` | int | Total number of feedback items in the system. This includes all complaints and feature requests. |
| `pendingVerificationRequests` | int | Total number of companies currently waiting for verification or approval. |
| `unreadFeedbacks` | int | Total number of feedback items that have not been read yet. |
| `totalPublishedJobs` | int | Total number of jobs currently stored in the jobs table. |
| `companiesRegisteredThisMonth` | int | Number of companies created during the current calendar month. |
| `newFeedbacksThisWeek` | int | Number of feedback items created during the current week. |

## Card Mapping For Frontend

Use the response fields for the dashboard cards in this mapping:

1. `إجمالي الشركات` → `totalCompanies`
2. `إجمالي الملاحظات` → `totalFeedbacks`
3. `طلبات التوثيق المعلقة` → `pendingVerificationRequests`
4. `الملاحظات غير المقروءة` → `unreadFeedbacks`
5. `إجمالي الوظائف المنشورة` → `totalPublishedJobs`
6. `الشركات المسجلة هذا الشهر` → `companiesRegisteredThisMonth`
7. `الملاحظات الجديدة هذا الأسبوع` → `newFeedbacksThisWeek`

## Business Notes

- `totalFeedbacks` is a combined total of all feedback records.
- `pendingVerificationRequests` is based on companies whose status is pending approval.
- `companiesRegisteredThisMonth` is based on `Employer.CreatedAt`.
- `newFeedbacksThisWeek` is based on `Feedback.CreatedAt`.
- `totalPublishedJobs` currently counts all rows in the jobs table.

## Frontend Integration Notes

- Call this endpoint once when the dashboard page loads.
- This endpoint is intended for dashboard summary cards only.
- Do not use the company page statistics endpoint for these dashboard cards.
- Do not use the feedback page statistics endpoint for these dashboard cards.
- No request body is needed.
- No query string is needed.
- The values are already aggregated by the backend and can be displayed directly.

## Suggested Frontend Request

### Cookie-based dashboard request

```http
GET /api/Admin/dashboard-statistics
Cookie: access_token=<dashboard_auth_cookie>
```

### Bearer token request

```http
GET /api/Admin/dashboard-statistics
Authorization: Bearer <your_token>
```

## Example JavaScript

```javascript
const response = await fetch('/api/Admin/dashboard-statistics', {
  method: 'GET',
  credentials: 'include'
});

const result = await response.json();
const stats = result.data;

const cards = {
  totalCompanies: stats.totalCompanies,
  totalFeedbacks: stats.totalFeedbacks,
  pendingVerificationRequests: stats.pendingVerificationRequests,
  unreadFeedbacks: stats.unreadFeedbacks,
  totalPublishedJobs: stats.totalPublishedJobs,
  companiesRegisteredThisMonth: stats.companiesRegisteredThisMonth,
  newFeedbacksThisWeek: stats.newFeedbacksThisWeek
};
```

## Notes For Another Model

If another model is integrating this page, it should assume:

- this endpoint is only for the shared dashboard summary cards
- this endpoint is shared between `Admin` and `SubAdmin`
- this endpoint already combines data from companies, feedbacks, and jobs
- the values are ready to render directly and do not need extra client-side aggregation
- separate management pages such as company management and feedback management should continue using their own dedicated endpoints
