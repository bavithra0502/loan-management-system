import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../../services/auth.service';
import { UserService } from '../../services/user.service';
import { LoanRequestService } from '../../services/loan-request.service';
import { LoanOfficerService } from '../../services/loan-officer.service';
import { BackgroundVerificationService } from '../../services/background-verification.service';
import { LoanVerificationService } from '../../services/loan-verification.service';
import { FeedbackQuestionService } from '../../services/feedback-question.service';
import { HelpReportService } from '../../services/help-report.service';
import { FeedbackService } from '../../services/feedback.service';
import { User } from '../../models/user.model';
import { LoanRequest } from '../../models/loan-request.model';
import { LoanOfficer } from '../../models/loan-officer.model';
import { FeedbackQuestion } from '../../models/feedback-question.model';
import { HelpReport } from '../../models/help-report.model';
import { BackgroundVerification } from '../../models/background-verification.model';
import { LoanVerification } from '../../models/loan-verification.model';
import { Feedback } from '../../models/feedback.model';

@Component({
  selector: 'app-admin-dashboard',
  templateUrl: './admin-dashboard.component.html'
})
export class AdminDashboardComponent implements OnInit {
  tab: 'users' | 'loans' | 'bgverify' | 'loanverify' | 'questions' | 'help' | 'feedback' = 'users';

  users: User[] = [];
  loanRequests: LoanRequest[] = [];
  officers: LoanOfficer[] = [];
  questions: FeedbackQuestion[] = [];
  helpReports: HelpReport[] = [];
  backgroundVerifications: BackgroundVerification[] = [];
  loanVerifications: LoanVerification[] = [];
  feedbacks: Feedback[] = [];

  selectedOfficerByLoan: { [loanRequestId: number]: number } = {};
  newQuestionText = '';
  editingQuestionId: number | null = null;
  editingQuestionText = '';
  replyText: { [id: number]: string } = {};

  remarksByBgId: { [id: number]: string } = {};
  statusByBgId: { [id: number]: string } = {};

  remarksByLvId: { [id: number]: string } = {};
  statusByLvId: { [id: number]: string } = {};
  resultByLvId: { [id: number]: string } = {};

  message = '';

  constructor(
    private auth: AuthService,
    private router: Router,
    private userService: UserService,
    private loanRequestService: LoanRequestService,
    private loanOfficerService: LoanOfficerService,
    private backgroundVerificationService: BackgroundVerificationService,
    private loanVerificationService: LoanVerificationService,
    private feedbackQuestionService: FeedbackQuestionService,
    private helpReportService: HelpReportService,
    private feedbackService: FeedbackService
  ) {}

  ngOnInit(): void {
    this.loadUsers();
    this.loadLoanRequests();
    this.loadOfficers();
    this.loadQuestions();
    this.loadHelpReports();
    this.loadBackgroundVerifications();
    this.loadLoanVerifications();
    this.loadFeedbacks();
  }

  setTab(tab: 'users' | 'loans' | 'bgverify' | 'loanverify' | 'questions' | 'help' | 'feedback'): void {
    this.tab = tab;
  }

  logout(): void {
    this.auth.logout();
    this.router.navigate(['/login']);
  }

  // ---- Users ----
  loadUsers(): void {
    this.userService.getAll().subscribe(res => (this.users = res));
  }

  approveUser(user: User): void {
    this.userService.updateStatus(user.userId, 'Approved').subscribe(() => this.loadUsers());
  }

  rejectUser(user: User): void {
    this.userService.updateStatus(user.userId, 'Rejected').subscribe(() => this.loadUsers());
  }

  deleteUser(user: User): void {
    if (!confirm(`Delete user "${user.userName}"?`)) return;
    this.userService.delete(user.userId).subscribe(() => this.loadUsers());
  }

  // ---- Loan Requests ----
  loadLoanRequests(): void {
    this.loanRequestService.getAll().subscribe(res => (this.loanRequests = res));
  }

  loadOfficers(): void {
    this.loanOfficerService.getAll().subscribe(res => (this.officers = res));
  }

  approveLoan(loan: LoanRequest): void {
    this.loanRequestService.updateStatus(loan.loanRequestId, 'Approved').subscribe(() => this.loadLoanRequests());
  }

  rejectLoan(loan: LoanRequest): void {
    this.loanRequestService.updateStatus(loan.loanRequestId, 'Rejected').subscribe(() => this.loadLoanRequests());
  }

  assignVerification(loan: LoanRequest): void {
    const officerId = this.selectedOfficerByLoan[loan.loanRequestId];
    if (!officerId) {
      this.message = 'Pick an officer first.';
      return;
    }
    this.backgroundVerificationService.assign(loan.loanRequestId, officerId).subscribe(() => {
      this.loanVerificationService.assign(loan.loanRequestId, officerId).subscribe(() => {
        this.message = `Officer assigned to loan #${loan.loanRequestId} for background + loan verification.`;
        this.loadBackgroundVerifications();
        this.loadLoanVerifications();
      });
    });
  }

  // ---- Background Verifications (view/update/delete) ----
  loadBackgroundVerifications(): void {
    this.backgroundVerificationService.getAll().subscribe(res => (this.backgroundVerifications = res));
  }

  updateBackgroundVerification(bv: BackgroundVerification): void {
    const status = this.statusByBgId[bv.verificationId] || bv.status;
    const remarks = this.remarksByBgId[bv.verificationId] ?? bv.remarks;
    this.backgroundVerificationService.update(bv.verificationId, status, remarks).subscribe(() => {
      this.message = `Background verification #${bv.verificationId} updated.`;
      this.loadBackgroundVerifications();
    });
  }

  deleteBackgroundVerification(bv: BackgroundVerification): void {
    if (!confirm(`Delete background verification #${bv.verificationId}?`)) return;
    this.backgroundVerificationService.delete(bv.verificationId).subscribe(() => this.loadBackgroundVerifications());
  }

  // ---- Loan Verifications (view/update/delete) ----
  loadLoanVerifications(): void {
    this.loanVerificationService.getAll().subscribe(res => (this.loanVerifications = res));
  }

  updateLoanVerification(lv: LoanVerification): void {
    const status = this.statusByLvId[lv.loanVerificationId] || lv.status;
    const remarks = this.remarksByLvId[lv.loanVerificationId] ?? lv.remarks;
    const result = this.resultByLvId[lv.loanVerificationId] ?? lv.verificationResult;
    this.loanVerificationService.update(lv.loanVerificationId, result, status, remarks).subscribe(() => {
      this.message = `Loan verification #${lv.loanVerificationId} updated.`;
      this.loadLoanVerifications();
    });
  }

  deleteLoanVerification(lv: LoanVerification): void {
    if (!confirm(`Delete loan verification #${lv.loanVerificationId}?`)) return;
    this.loanVerificationService.delete(lv.loanVerificationId).subscribe(() => this.loadLoanVerifications());
  }

  // ---- Feedback Questions ----
  loadQuestions(): void {
    this.feedbackQuestionService.getAll().subscribe(res => (this.questions = res));
  }

  addQuestion(): void {
    if (!this.newQuestionText.trim()) return;
    this.feedbackQuestionService.add({ question: this.newQuestionText, isActive: true }).subscribe(() => {
      this.newQuestionText = '';
      this.loadQuestions();
    });
  }

  toggleQuestion(q: FeedbackQuestion): void {
    const updated = { ...q, isActive: !q.isActive };
    this.feedbackQuestionService.update(updated).subscribe(() => this.loadQuestions());
  }

  startEditQuestion(q: FeedbackQuestion): void {
    this.editingQuestionId = q.questionId;
    this.editingQuestionText = q.question;
  }

  saveEditQuestion(q: FeedbackQuestion): void {
    if (!this.editingQuestionText.trim()) return;
    const updated = { ...q, question: this.editingQuestionText };
    this.feedbackQuestionService.update(updated).subscribe(() => {
      this.editingQuestionId = null;
      this.loadQuestions();
    });
  }

  cancelEditQuestion(): void {
    this.editingQuestionId = null;
  }

  // ---- Customer Feedback (view answers) ----
  loadFeedbacks(): void {
    this.feedbackService.getAll().subscribe(res => (this.feedbacks = res));
  }

  questionText(questionId: number): string {
    return this.questions.find(q => q.questionId === questionId)?.question ?? `Question #${questionId}`;
  }

  // ---- Help Reports ----
  loadHelpReports(): void {
    this.helpReportService.getAll().subscribe(res => (this.helpReports = res));
  }

  sendReply(report: HelpReport): void {
    const reply = this.replyText[report.helpReportId];
    if (!reply || !reply.trim()) return;
    this.helpReportService.reply(report.helpReportId, reply, 'Closed').subscribe(() => {
      this.replyText[report.helpReportId] = '';
      this.loadHelpReports();
    });
  }
}
