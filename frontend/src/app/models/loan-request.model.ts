export interface LoanRequest {
  loanRequestId: number;
  customerId: number;
  loanType: string;
  loanAmount: number;
  loanPeriod: number;
  purpose: string;
  applyDate?: string;
  status: string;
}
