# GoWork API Documentation

A comprehensive API reference for frontend developers integrating with the GoWork job platform.

---

## Table of Contents

1. [Introduction](#introduction)
2. [Base URL](#base-url)
3. [Authentication](#authentication)
4. [Standard Response Format](#standard-response-format)
5. [Authentication Endpoints](#authentication-endpoints)
6. [Profile Management Endpoints](#profile-management-endpoints)
7. [File Management Endpoints](#file-management-endpoints)
8. [Password Recovery Endpoints](#password-recovery-endpoints)
9. [Admin & Test Endpoints](#admin--test-endpoints)
10. [Error Codes Reference](#error-codes-reference)

---

## Introduction

GoWork is a job platform API that supports:
- **Job Seekers (Candidates):** Registration, profile management, resume upload
- **Employers (Companies):** Company registration, job posting management
- **Dual Authentication:** JWT tokens for mobile apps, HTTP-only cookies for web dashboard

---

## Base URL

```
Production: https://api.gowork.com
Development: https://localhost:5001
```

All endpoints are prefixed with `/api/`.

---

## Authentication

### JWT Bearer Token (Mobile Apps)

Include the token in the `Authorization` header:

```http
Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...
```

### HTTP-Only Cookie (Web Dashboard)

For company login via web, the `access_token` cookie is automatically set and sent with requests:

```http
Cookie: access_token=eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...
```

### Roles

| Role | Description |
|------|-------------|
| `Admin` | System administrator |
| `Company` | Employer/Company user |
| `Candidate` | Job seeker |

---

## Standard Response Format

All API responses use the `ApiResponse<T>` wrapper:

### Success Response

```json
{
  "statusCode": 200,
  "success": true,
  "data": { ... },
  "errors": []
}
```

### Error Response

```json
{
  "statusCode": 400,
  "success": false,
  "data": null,
  "errors": ["Error message 1", "Error message 2"]
}
```

---

## Authentication Endpoints

### POST `/api/Account/Candidate/Register`

Register a new job seeker account.

**Authentication:** None (Public)

**Content-Type:** `multipart/form-data`

#### Request Body

| Field | Type | Required | Constraints | Description |
|-------|------|----------|-------------|-------------|
| `FirstName` | string | ✅ | 2-50 chars | Candidate's first name |
| `MidName` | string | ❌ | max 50 chars | Middle name |
| `LastName` | string | ✅ | 2-50 chars | Candidate's last name |
| `Email` | string | ✅ | Valid email, max 100 chars | Email address |
| `PhoneNumber` | string | ✅ | Valid phone, max 20 chars | Phone number |
| `Password` | string | ✅ | 8-100 chars | Account password |
| `PasswordConfirmation` | string | ✅ | Must match Password | Password confirmation |
| `ProfilePhoto` | file | ❌ | Image file | Profile picture |
| `Resume` | file | ❌ | PDF/DOC file | Resume document |
| `ListOfSkills` | string[] | ✅ | Min 1 skill | List of skills |
| `InterstedInCategoryId` | string | ✅ | - | Category ID of interest |

#### Success Response (200)

```json
{
  "statusCode": 200,
  "success": true,
  "data": {
    "candidateId": 1,
    "email": "john@example.com",
    "sasUrl": "https://storage.blob.core.windows.net/...",
    "expiresAt": "2026-02-10T16:00:00Z",
    "role": "Candidate",
    "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9..."
  },
  "errors": []
}
```

#### Error Responses

| Status | Description |
|--------|-------------|
| 400 | Invalid registration data |
| 409 | Email already exists |

#### JavaScript Example

```javascript
const formData = new FormData();
formData.append('FirstName', 'John');
formData.append('LastName', 'Doe');
formData.append('Email', 'john@example.com');
formData.append('PhoneNumber', '+1234567890');
formData.append('Password', 'SecurePass123!');
formData.append('PasswordConfirmation', 'SecurePass123!');
formData.append('ListOfSkills', 'JavaScript');
formData.append('ListOfSkills', 'React');
formData.append('InterstedInCategoryId', '1');

const response = await fetch('/api/Account/Candidate/Register', {
  method: 'POST',
  body: formData
});

const result = await response.json();
```

---

### POST `/api/Account/Register`

Register a new employer/company account.

**Authentication:** None (Public)

**Content-Type:** `multipart/form-data`

#### Request Body

| Field | Type | Required | Constraints | Description |
|-------|------|----------|-------------|-------------|
| `CompanyName` | string | ✅ | 2-100 chars | Company name |
| `Email` | string | ✅ | Valid email, max 100 chars | Company email |
| `Password` | string | ✅ | 8-100 chars | Account password |
| `PasswordConfirmation` | string | ✅ | Must match Password | Confirmation |
| `PhoneNumber` | string | ✅ | Valid phone, max 20 chars | Contact phone |
| `Industry` | string | ✅ | max 100 chars | Industry sector |
| `LogoUrl` | file | ❌ | Image file | Company logo |

#### Success Response (200)

```json
{
  "statusCode": 200,
  "success": true,
  "data": {
    "employerId": 1,
    "email": "company@example.com",
    "sasUrl": "https://storage.blob.core.windows.net/...",
    "expiresAt": "2026-02-10T16:00:00Z",
    "role": "Company",
    "companyName": "Tech Corp"
  },
  "errors": []
}
```

#### JavaScript Example

```javascript
const formData = new FormData();
formData.append('CompanyName', 'Tech Corp');
formData.append('Email', 'hr@techcorp.com');
formData.append('Password', 'SecurePass123!');
formData.append('PasswordConfirmation', 'SecurePass123!');
formData.append('PhoneNumber', '+1234567890');
formData.append('Industry', 'Technology');
// formData.append('LogoUrl', logoFile); // Optional

const response = await fetch('/api/Account/Register', {
  method: 'POST',
  body: formData
});
```

---

### POST `/api/Account/Candidate/VerifyEmail`

Verify candidate email using OTP code.

**Authentication:** None (Public)

**Content-Type:** `application/json`

#### Request Body

```json
{
  "email": "john@example.com",
  "emailConfirmationCode": "123456"
}
```

| Field | Type | Required | Constraints | Description |
|-------|------|----------|-------------|-------------|
| `Email` | string | ✅ | Valid email, max 100 chars | Registered email |
| `EmailConfirmationCode` | string | ✅ | 6 chars | OTP sent to email |

#### Success Response (200)

```json
{
  "statusCode": 200,
  "success": true,
  "data": {
    "candidateId": 1,
    "email": "john@example.com",
    "sasUrl": "...",
    "expiresAt": "2026-02-10T16:00:00Z",
    "role": "Candidate",
    "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9..."
  },
  "errors": []
}
```

#### JavaScript Example

```javascript
const response = await fetch('/api/Account/Candidate/VerifyEmail', {
  method: 'POST',
  headers: { 'Content-Type': 'application/json' },
  body: JSON.stringify({
    email: 'john@example.com',
    emailConfirmationCode: '123456'
  })
});
```

---

### POST `/api/Account/VerifyEmail`

Verify company email using OTP code. Sets HTTP-only cookie.

**Authentication:** None (Public)

**Content-Type:** `application/json`

#### Request Body

```json
{
  "email": "company@example.com",
  "emailConfirmationCode": "123456"
}
```

#### Success Response (200)

Returns `EmployerResponseDTO` and sets `access_token` cookie (7-day expiry).

```json
{
  "statusCode": 200,
  "success": true,
  "data": {
    "employerId": 1,
    "email": "company@example.com",
    "sasUrl": "...",
    "expiresAt": "2026-02-10T16:00:00Z",
    "role": "Company",
    "companyName": "Tech Corp"
  },
  "errors": []
}
```

---

### POST `/api/Account/Candidate/Login`

Authenticate a job seeker.

**Authentication:** None (Public)

**Content-Type:** `application/json`

#### Request Body

```json
{
  "email": "john@example.com",
  "password": "SecurePass123!"
}
```

| Field | Type | Required | Constraints | Description |
|-------|------|----------|-------------|-------------|
| `Email` | string | ✅ | Valid email, max 100 chars | Account email |
| `Password` | string | ✅ | 8-100 chars | Account password |

#### Success Response (200)

```json
{
  "statusCode": 200,
  "success": true,
  "data": {
    "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9..."
  },
  "errors": []
}
```

#### Error Responses

| Status | Description |
|--------|-------------|
| 400 | Invalid login data |
| 401 | Invalid credentials or email not verified |

#### JavaScript Example

```javascript
const response = await fetch('/api/Account/Candidate/Login', {
  method: 'POST',
  headers: { 'Content-Type': 'application/json' },
  body: JSON.stringify({
    email: 'john@example.com',
    password: 'SecurePass123!'
  })
});

const { data } = await response.json();
// Store token for subsequent requests
localStorage.setItem('authToken', data.token);
```

---

### POST `/api/Account/Login`

Authenticate a company user. Sets HTTP-only cookie.

**Authentication:** None (Public)

**Content-Type:** `application/json`

#### Request Body

```json
{
  "email": "hr@techcorp.com",
  "password": "SecurePass123!"
}
```

#### Success Response (200)

Returns `EmployerResponseDTO` and sets `access_token` cookie (7-day expiry).

```json
{
  "statusCode": 200,
  "success": true,
  "data": {
    "employerId": 1,
    "email": "hr@techcorp.com",
    "sasUrl": "...",
    "expiresAt": "2026-02-10T16:00:00Z",
    "role": "Company",
    "companyName": "Tech Corp"
  },
  "errors": []
}
```

#### JavaScript Example (Web Dashboard)

```javascript
const response = await fetch('/api/Account/Login', {
  method: 'POST',
  headers: { 'Content-Type': 'application/json' },
  credentials: 'include', // Important for cookies
  body: JSON.stringify({
    email: 'hr@techcorp.com',
    password: 'SecurePass123!'
  })
});
```

---

### POST `/api/Account/Logout`

Logout current user. Clears authentication cookie.

**Authentication:** Required (Bearer Token or Cookie)

**Content-Type:** None

#### Success Response (200)

```json
"Logout Successfully"
```

#### JavaScript Example

```javascript
await fetch('/api/Account/Logout', {
  method: 'POST',
  credentials: 'include',
  headers: {
    'Authorization': `Bearer ${token}` // For mobile
  }
});
```

---

## Profile Management Endpoints

### GET `/api/Account/Me`

Get current authenticated user's role.

**Authentication:** Required

#### Success Response (200)

```json
{
  "role": "Candidate"
}
```

#### JavaScript Example

```javascript
const response = await fetch('/api/Account/Me', {
  headers: { 'Authorization': `Bearer ${token}` }
});

const { role } = await response.json();
```

---

### GET `/api/Account/candidate/profile/me`

Get full profile of authenticated candidate.

**Authentication:** Required (Candidate role)

#### Success Response (200)

```json
{
  "statusCode": 200,
  "success": true,
  "data": {
    "firstName": "John",
    "middleName": null,
    "lastName": "Doe",
    "profilPhotoUrl": "https://storage.blob.core.windows.net/...",
    "resumeUrl": "https://storage.blob.core.windows.net/...",
    "skills": ["JavaScript", "React", "Node.js"]
  },
  "errors": []
}
```

#### JavaScript Example

```javascript
const response = await fetch('/api/Account/candidate/profile/me', {
  headers: { 'Authorization': `Bearer ${token}` }
});

const { data: profile } = await response.json();
console.log(profile.firstName, profile.skills);
```

---

### PATCH `/api/Account/Candidate/UpdateProfile`

Update candidate profile information.

**Authentication:** Required (Candidate role)

**Content-Type:** `multipart/form-data`

#### Request Body

| Field | Type | Required | Description |
|-------|------|----------|-------------|
| `FirstName` | string | ❌ | Updated first name |
| `MiddleName` | string | ❌ | Updated middle name |
| `LastName` | string | ❌ | Updated last name |
| `ProfilePhoto` | file | ❌ | New profile photo |
| `ResumeFile` | file | ❌ | New resume document |
| `Skills` | string[] | ❌ | Updated skills list |

> **Note:** Only include fields you want to update.

#### Success Response (200)

```json
{
  "statusCode": 200,
  "success": true,
  "data": {
    "firstName": "John",
    "middleName": "William",
    "lastName": "Doe",
    "emial": "john@example.com",
    "profilePhotoUrl": "https://...",
    "expiresAt": "2026-02-10T16:00:00Z",
    "skills": ["JavaScript", "React", "TypeScript"],
    "role": "Candidate"
  },
  "errors": []
}
```

#### JavaScript Example

```javascript
const formData = new FormData();
formData.append('FirstName', 'John');
formData.append('Skills', 'JavaScript');
formData.append('Skills', 'TypeScript');
formData.append('Skills', 'React');

const response = await fetch('/api/Account/Candidate/UpdateProfile', {
  method: 'PATCH',
  headers: { 'Authorization': `Bearer ${token}` },
  body: formData
});
```

---

## File Management Endpoints

### POST `/api/Account/UploadResume`

Upload a resume file for the user.

**Authentication:** Required

**Content-Type:** `multipart/form-data`

#### Request Body

| Field | Type | Required | Description |
|-------|------|----------|-------------|
| `File` | file | ✅ | Resume document (PDF, DOC, DOCX) |
| `UserId` | integer | ✅ | User ID |

#### Success Response (200)

```json
{
  "statusCode": 200,
  "success": true,
  "data": {
    "message": "Resume uploaded successfully"
  },
  "errors": []
}
```

---

### PUT `/api/Account/UpdateProfilePhoto`

Update user's profile photo.

**Authentication:** Required

**Content-Type:** `multipart/form-data`

#### Request Body

| Field | Type | Required | Description |
|-------|------|----------|-------------|
| `File` | file | ✅ | Image file (JPEG, PNG) |
| `UserId` | integer | ✅ | User ID |

#### Success Response (200)

```json
{
  "statusCode": 200,
  "success": true,
  "data": {
    "message": "File updated successfully"
  },
  "errors": []
}
```

---

### PUT `/api/Account/UpdateResume`

Update user's resume file.

**Authentication:** Required

**Content-Type:** `multipart/form-data`

#### Request Body

| Field | Type | Required | Description |
|-------|------|----------|-------------|
| `File` | file | ✅ | Resume document |
| `UserId` | integer | ✅ | User ID |

---

### GET `/api/Account/DownloadProfilePhoto/{UserId}`

Get a signed URL to download user's profile photo.

**Authentication:** Required

**Path Parameters:**

| Parameter | Type | Description |
|-----------|------|-------------|
| `UserId` | integer | Target user ID |

#### Success Response (200)

```json
{
  "statusCode": 200,
  "success": true,
  "data": {
    "sasUrl": "https://storage.blob.core.windows.net/.../photo.jpg?...",
    "expiresAt": "2026-02-03T18:00:00Z",
    "succeeded": true
  },
  "errors": []
}
```

#### JavaScript Example

```javascript
const response = await fetch(`/api/Account/DownloadProfilePhoto/${userId}`, {
  headers: { 'Authorization': `Bearer ${token}` }
});

const { data } = await response.json();

// Use the SAS URL directly in an <img> tag
document.getElementById('avatar').src = data.sasUrl;
```

---

### GET `/api/Account/DownloadResume/{UserId}`

Get a signed URL to download user's resume.

**Authentication:** Required

**Path Parameters:**

| Parameter | Type | Description |
|-----------|------|-------------|
| `UserId` | integer | Target user ID |

#### Success Response (200)

```json
{
  "statusCode": 200,
  "success": true,
  "data": {
    "sasUrl": "https://storage.blob.core.windows.net/.../resume.pdf?...",
    "expiresAt": "2026-02-03T18:00:00Z",
    "succeeded": true
  },
  "errors": []
}
```

---

### POST `/api/Files/Upload`

General file upload endpoint.

**Authentication:** None (Public)

**Content-Type:** `multipart/form-data`

#### Request Body

| Field | Type | Required | Description |
|-------|------|----------|-------------|
| `File` | file | ✅ | File to upload |
| `UserId` | integer | ✅ | Associated user ID |

#### Success Response (200)

```json
{
  "statusCode": 200,
  "success": true,
  "data": {
    "message": "File uploaded successfully"
  },
  "errors": []
}
```

---

### GET `/api/Files/ProfilePhoto`

Get profile photo URL by user ID.

**Authentication:** None (Public)

#### Query Parameters

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `UserId` | integer | ✅ | Target user ID |

#### Success Response (200)

```json
{
  "statusCode": 200,
  "success": true,
  "data": {
    "sasUrl": "https://storage.blob.core.windows.net/...",
    "expiresAt": "2026-02-03T18:00:00Z",
    "succeeded": true
  },
  "errors": []
}
```

---

## Password Recovery Endpoints

### POST `/api/Account/ForgetPassword`

Request a password reset email.

**Authentication:** None (Public)

**Content-Type:** `application/json`

#### Request Body

```json
{
  "email": "john@example.com"
}
```

| Field | Type | Required | Description |
|-------|------|----------|-------------|
| `Email` | string | ✅ | Registered email address |

#### Success Response (200)

```json
{
  "statusCode": 200,
  "success": true,
  "data": {
    "message": "Password reset email sent"
  },
  "errors": []
}
```

#### JavaScript Example

```javascript
await fetch('/api/Account/ForgetPassword', {
  method: 'POST',
  headers: { 'Content-Type': 'application/json' },
  body: JSON.stringify({ email: 'john@example.com' })
});
```

---

### POST `/api/Account/ResetPassword`

Reset password using token from email.

**Authentication:** None (Public)

**Content-Type:** `application/json`

#### Request Body

```json
{
  "email": "john@example.com",
  "token": "CfDJ8NrAkzBT...",
  "newPassword": "NewSecurePass456!"
}
```

| Field | Type | Required | Description |
|-------|------|----------|-------------|
| `Email` | string | ✅ | Account email |
| `Token` | string | ✅ | Reset token from email link |
| `NewPassword` | string | ✅ | New password (min 8 chars) |

#### Success Response (200)

```json
{
  "statusCode": 200,
  "success": true,
  "data": {
    "message": "Password reset successfully"
  },
  "errors": []
}
```

#### JavaScript Example

```javascript
// Extract token from URL query parameter
const urlParams = new URLSearchParams(window.location.search);
const token = urlParams.get('token');
const email = urlParams.get('email');

await fetch('/api/Account/ResetPassword', {
  method: 'POST',
  headers: { 'Content-Type': 'application/json' },
  body: JSON.stringify({
    email: email,
    token: token,
    newPassword: 'NewSecurePass456!'
  })
});
```

---

## Admin & Test Endpoints

### GET `/api/Account/AdminTest`

Test endpoint for admin role verification.

**Authentication:** Required (Admin role)

#### Success Response (200)

```json
"Welcome Admin"
```

---

### GET `/api/Account/CompanyTest`

Test endpoint for company role verification.

**Authentication:** Required (Company role)

#### Success Response (200)

```json
"Welcome Company"
```

---

### GET `/api/Company/test-auth`

Detailed authentication test for admin users.

**Authentication:** Required (Admin role)

#### Success Response (200)

```json
{
  "message": "Authentication successful",
  "userId": "123",
  "email": "admin@gowork.com",
  "roles": ["Admin"]
}
```

---

## Error Codes Reference

| HTTP Status | Meaning | Common Causes |
|-------------|---------|---------------|
| 200 | Success | Request completed successfully |
| 201 | Created | Resource created successfully |
| 400 | Bad Request | Invalid input, validation errors |
| 401 | Unauthorized | Missing/invalid token, unverified email |
| 403 | Forbidden | Insufficient role permissions |
| 404 | Not Found | Resource doesn't exist |
| 409 | Conflict | Duplicate resource (e.g., email exists) |
| 500 | Server Error | Internal server error |

### Common Error Messages

| Error | Endpoint | Meaning |
|-------|----------|---------|
| `"Invalid registration data."` | Register | Form validation failed |
| `"Invalid Login data."` | Login | Email/password validation failed |
| `"Invalid Confirmation data."` | Verify Email | OTP validation failed |
| `"Unauthorized: CandidateId not found."` | Profile endpoints | Invalid or missing JWT |
| `"Invalid UserId."` | File download | UserId ≤ 0 |

---

## Quick Reference

### Headers for Authenticated Requests

```javascript
// Mobile App (JWT)
headers: {
  'Authorization': `Bearer ${token}`,
  'Content-Type': 'application/json'
}

// Web Dashboard (Cookies)
credentials: 'include'
```

### File Upload Template

```javascript
const uploadFile = async (endpoint, file, userId, token) => {
  const formData = new FormData();
  formData.append('File', file);
  formData.append('UserId', userId);

  const response = await fetch(endpoint, {
    method: 'POST',
    headers: { 'Authorization': `Bearer ${token}` },
    body: formData
  });

  return response.json();
};
```

### Error Handling Template

```javascript
const apiCall = async (url, options) => {
  const response = await fetch(url, options);
  const result = await response.json();

  if (!result.success) {
    throw new Error(result.errors.join(', '));
  }

  return result.data;
};
```

---

*Documentation generated on February 3, 2026*
