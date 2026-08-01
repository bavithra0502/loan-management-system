import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../../services/auth.service';
import { CustomerService } from '../../services/customer.service';
import { LoanRequestService } from '../../services/loan-request.service';
import { FeedbackQuestionService } from '../../services/feedback-question.service';
import { FeedbackService } from '../../services/feedback.service';
import { HelpReportService } from '../../services/help-report.service';
import { Customer } from '../../models/customer.model';
import { LoanRequest } from '../../models/loan-request.model';
import { FeedbackQuestion } from '../../models/feedback-question.model';
import { HelpReport } from '../../models/help-report.model';

@Component({
  selector: 'app-customer-dashboard',
  templateUrl: './customer-dashboard.component.html'
})
export class CustomerDashboardComponent implements OnInit {
  tab: 'loans' | 'apply' | 'feedback' | 'help' = 'loans';

  customer: Customer | null = null;
  loanRequests: LoanRequest[] = [];
  questions: FeedbackQuestion[] = [];
  helpReports: HelpReport[] = [];

  newLoan = { loanType: 'Home', loanAmount: 0, loanPeriod: 12, purpose: '' };
  feedbackAnswers: { [questionId: number]: string } = {};
  newHelp = { subject: '', description: '' };

  message = '';
  error = '';

  constructor(
    private auth: AuthService,
    private router: Router,
    private customerService: CustomerService,
    private loanRequestService: LoanRequestService,
    private feedbackQuestionService: FeedbackQuestionService,
    private feedbackService: FeedbackService,
    private helpReportService: HelpReportService
  ) {}

  ngOnInit(): void {
    const user = this.auth.getCurrentUser();
    if (!user) return;

    this.customerService.getByUserId(user.userId).subscribe(c => {
      this.customer = c;
      this.loadLoans();
    });
    this.feedbackQuestionService.getActive().subscribe(res => (this.questions = res));
    this.helpReportService.getByUser(user.userId).subscribe(res => (this.helpReports = res));
  }

  setTab(tab: 'loans' | 'apply' | 'feedback' | 'help'): void {
    this.tab = tab;
  }

  logout(): void {
    this.auth.logout();
    this.router.navigate(['/login']);
  }

  loadLoans(): void {
    if (!this.customer) return;
    this.loanRequestService.getByCustomer(this.customer.customerId).subscribe(res => (this.loanRequests = res));
  }

  applyLoan(): void {
    if (!this.customer) return;
    this.error = '';
    this.message = '';
    const payload = { ...this.newLoan, customerId: this.customer.customerId };
    this.loanRequestService.apply(payload).subscribe({
      next: () => {
        this.message = 'Loan application submitted!';
        this.newLoan = { loanType: 'Home', loanAmount: 0, loanPeriod: 12, purpose: '' };
        this.loadLoans();
        this.tab = 'loans';
      },
      error: err => (this.error = err.error?.message || 'Could not submit loan application.')
    });
  }

  submitFeedback(q: FeedbackQuestion): void {
    if (!this.customer) return;
    const answer = this.feedbackAnswers[q.questionId];
    if (!answer || !answer.trim()) return;
    this.feedbackService
      .add({ customerId: this.customer.customerId, questionId: q.questionId, answer })
      .subscribe(() => {
        this.message = 'Thanks for your feedback!';
        this.feedbackAnswers[q.questionId] = '';
      });
  }

  submitHelp(): void {
    const user = this.auth.getCurrentUser();
    if (!user || !this.newHelp.subject.trim()) return;
    this.helpReportService
      .create({ userId: user.userId, subject: this.newHelp.subject, description: this.newHelp.description })
      .subscribe(() => {
        this.message = 'Help request submitted!';
        this.newHelp = { subject: '', description: '' };
        this.helpReportService.getByUser(user.userId).subscribe(res => (this.helpReports = res));
      });
  }
}
