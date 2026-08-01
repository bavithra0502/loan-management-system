export interface User {
  userId: number;
  userName: string;
  password?: string;
  role: string; // Admin | Customer | LoanOfficer
  status: string; // Pending | Approved | Rejected
  createdDate?: string;
}

export interface LoginResponse {
  userId: number;
  userName: string;
  uName: string;
  role: string;
  roleId: number;
  status: string;
  token: string;
}
