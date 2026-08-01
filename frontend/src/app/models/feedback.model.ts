export interface Feedback {
  feedbackId: number;
  customerId: number;
  questionId: number;
  answer: string;
  feedbackDate?: string;
}
