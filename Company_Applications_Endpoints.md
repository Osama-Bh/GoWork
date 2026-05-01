# Company Job Applications API Documentation

These endpoints allow a company (Employer) to manage the applications submitted by candidates for their jobs. The endpoints are secured and require the user to have the `Company` role.

---

## 1. Get Job Applications (Paginated & Filtered)

Retrieves a paginated list of all candidates who applied to the logged-in company's jobs. This supports search by candidate name/email, and filtering by Job or Status.

**Endpoint:** `GET /api/Applications/applications`
**Authorization:** Bearer Token required (Role: `Company`)

### Query Parameters

| Parameter | Type | Required | Description |
| :--- | :--- | :--- | :--- |
| `SearchTerm` | string | No | Search by candidate's First Name, Last Name, or Email. |
| `JobId` | int | No | Filter applications for a specific Job ID. |
| `StatusId` | int | No | Filter applications by Status (e.g., 1 for PendingReview, 3 for Rejected, 4 for Accepted). |
| `Page` | int | No | The page number for pagination. Default is `1`. |
| `PageSize` | int | No | Number of items per page. Default is `10`. |

### Example Request

```http
GET /api/Applications/applications?SearchTerm=سارة&JobId=5&StatusId=1&Page=1&PageSize=10
Authorization: Bearer <your_jwt_token>
```

### Successful Response (200 OK)

```json
{
  "items": [
    {
      "applicationId": 12,
      "candidateName": "سارة أحمد العلي",
      "candidateEmail": "sara.ali@email.com",
      "jobTitle": "مطور واجهات أمامية",
      "applicationDate": "2025-01-08T14:30:00Z",
      "candidateDescription": "مطور واجهات أمامية مع خبرة 3 سنوات في React و Vue.js",
      "resumeUrl": "https://storage.example.com/resumes/sara.pdf",
      "statusId": 1,
      "statusName": "PendingReview"
    }
  ],
  "currentPage": 1,
  "pageSize": 10,
  "totalCount": 1,
  "totalPages": 1
}
```

---

## 2. Update Application Status (Accept / Reject)

Updates the status of a specific candidate's application. A company can only update the status of applications made to their own jobs.

**Endpoint:** `PUT /api/Applications/applications/{applicationId}/status`
**Authorization:** Bearer Token required (Role: `Company`)

### Path Parameters

* `applicationId` (int): The unique ID of the application to update.

### Request Body (JSON)

```json
{
  "statusId": 4
}
```
*(Reference: `3` = Rejected, `4` = Accepted)*

### Example Request

```http
PUT /api/Applications/applications/12/status
Authorization: Bearer <your_jwt_token>
Content-Type: application/json

{
  "statusId": 4
}
```

### Responses

* **200 OK:**
  ```json
  { 
    "message": "Application status updated successfully." 
  }
  ```

* **400 Bad Request:** 
  ```json
  { 
    "message": "Failed to update status. Application not found, not authorized, or invalid status ID." 
  }
  ```

* **401 Unauthorized:**
  ```json
  {
    "message": "User ID not found or invalid."
  }
  ```
