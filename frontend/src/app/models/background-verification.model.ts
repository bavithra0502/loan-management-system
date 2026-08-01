export interface BackgroundVerification {
  verificationId: number;
  loanRequestId: number;
  officerId: number;
  verificationDate?: string;
  remarks: string;
  status: string;
}
