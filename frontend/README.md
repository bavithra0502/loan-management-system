# Loan Management System — Frontend (Angular)

A simple, non-fancy Angular app that talks to the `LoanManagementAPI` backend.
No UI library, no state-management library — just `HttpClient`, `FormsModule`
(template-driven forms), a JWT auth interceptor, and role-based route guards.

## Folder structure

```
frontend/
├── src/
│   ├── environments/
│   │   ├── environment.ts        <-- apiUrl for dev (ng serve)
│   │   └── environment.prod.ts   <-- apiUrl for production build
│   ├── app/
│   │   ├── models/                One TS interface per backend entity/DTO
│   │   ├── services/               One Angular service per controller (HttpClient calls)
│   │   ├── guards/                 auth.guard.ts (logged-in check), role.guard.ts (role check)
│   │   ├── interceptors/           auth.interceptor.ts (adds Bearer token to every request)
│   │   ├── components/
│   │   │   ├── login/
│   │   │   ├── register-customer/
│   │   │   ├── register-officer/
│   │   │   ├── admin/              Users, Loan Requests + assign officer, Feedback Qs, Help Reports
│   │   │   ├── customer/           My Loans, Apply, Feedback, Help/Support
│   │   │   └── officer/            Background Verifications, Loan Verifications, Help/Support
│   │   ├── app.component.ts
│   │   ├── app.module.ts
│   │   └── app-routing.module.ts
│   ├── index.html
│   ├── main.ts
│   └── styles.css
├── angular.json
├── package.json
├── tsconfig.json
└── tsconfig.app.json
```

## How it maps to the backend

| Backend controller              | Frontend service                       |
|----------------------------------|-----------------------------------------|
| `LoginsController`               | `auth.service.ts`                       |
| `UsersController`                | `user.service.ts`                       |
| `CustomersController`            | `customer.service.ts`                   |
| `LoanOfficersController`         | `loan-officer.service.ts`               |
| `LoanRequestsController`         | `loan-request.service.ts`               |
| `BackgroundVerificationsController` | `background-verification.service.ts` |
| `LoanVerificationsController`    | `loan-verification.service.ts`          |
| `HelpReportsController`          | `help-report.service.ts`                |
| `FeedbackQuestionsController`    | `feedback-question.service.ts`          |
| `FeedbacksController`            | `feedback.service.ts`                   |

`auth.service.ts` calls `GET Logins/{username}/{password}` exactly like the
README in the backend zip describes, stores the returned JWT + user info in
`localStorage`, and the `AuthInterceptor` attaches
`Authorization: Bearer <token>` to every subsequent request so the API's
`[Authorize(Roles = "...")]` attributes work.

## Setup

1. Install Node.js 18+ and the Angular CLI: `npm install -g @angular/cli`
2. From this `frontend/` folder: `npm install`
3. Check `src/environments/environment.ts` — set `apiUrl` to match the port
   printed when you run the backend with `dotnet run` (Swagger opens at the
   same host, e.g. `https://localhost:7000/api/`). Keep the trailing slash.
4. Run `ng serve` (or `npm start`) — the app opens on `http://localhost:4200`,
   which is the exact origin the backend's CORS policy (`AngularClient`)
   already allows.
5. Log in with the seeded admin account from the backend: `admin` / `Admin@123`,
   or register a new Customer / Loan Officer (they land in `Pending` status
   until the Admin approves them from the Users tab).

## Notes on "simple, not advanced"

- No NgRx/Signals/standalone components — classic `NgModule` + services +
  template-driven forms (`FormsModule` / `[(ngModel)]`).
- Each role dashboard is a single component with simple tab-switching in
  the template rather than a deep component tree or a UI kit.
- Styling is one plain `styles.css` file, no Tailwind/Bootstrap/Material.
