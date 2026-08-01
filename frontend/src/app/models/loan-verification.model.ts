export interface LoanVerification {
  loanVerificationId: number;
  loanRequestId: number;
  officerId: number;
  verificationDate?: string;
  verificationResult: string;
  remarks: string;
  status: string;
}
