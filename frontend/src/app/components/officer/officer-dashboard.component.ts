import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../../services/auth.service';
import { LoanOfficerService } from '../../services/loan-officer.service';
import { BackgroundVerificationService } from '../../services/background-verification.service';
import { LoanVerificationService } from '../../services/loan-verification.service';
import { HelpReportService } from '../../services/help-report.service';
import { LoanOfficer } from '../../models/loan-officer.model';
import { BackgroundVerification } from '../../models/background-verification.model';
import { LoanVerification } from '../../models/loan-verification.model';
import { HelpReport } from '../../models/help-report.model';

@Component({
  selector: 'app-officer-dashboard',
  templateUrl: './officer-dashboard.component.html'
})
export class OfficerDashboardComponent implements OnInit {
  tab: 'background' | 'loanverify' | 'help' = 'background';

  officer: LoanOfficer | null = null;
  backgroundVerifications: BackgroundVerification[] = [];
  loanVerifications: LoanVerification[] = [];
  helpReports: HelpReport[] = [];

  remarksByBgId: { [id: number]: string } = {};
  statusByBgId: { [id: number]: string } = {};

  remarksByLvId: { [id: number]: string } = {};
  statusByLvId: { [id: number]: string } = {};
  resultByLvId: { [id: number]: string } = {};

  newHelp = { subject: '', description: '' };
  message = '';

  constructor(
    private auth: AuthService,
    private router: Router,
    private loanOfficerService: LoanOfficerService,
    private backgroundVerificationService: BackgroundVerificationService,
    private loanVerificationService: LoanVerificationService,
    private helpReportService: HelpReportService
  ) {}

  ngOnInit(): void {
    const user = this.auth.getCurrentUser();
    if (!user) return;

    this.loanOfficerService.getByUserId(user.userId).subscribe(o => {
      this.officer = o;
      this.loadBackgroundVerifications();
      this.loadLoanVerifications();
    });
    this.helpReportService.getByUser(user.userId).subscribe(res => (this.helpReports = res));
  }

  setTab(tab: 'background' | 'loanverify' | 'help'): void {
    this.tab = tab;
  }

  logout(): void {
    this.auth.logout();
    this.router.navigate(['/login']);
  }

  loadBackgroundVerifications(): void {
    if (!this.officer) return;
    this.backgroundVerificationService.getByOfficer(this.officer.officerId).subscribe(res => (this.backgroundVerifications = res));
  }

  loadLoanVerifications(): void {
    if (!this.officer) return;
    this.loanVerificationService.getByOfficer(this.officer.officerId).subscribe(res => (this.loanVerifications = res));
  }

  updateBackground(bv: BackgroundVerification): void {
    const status = this.statusByBgId[bv.verificationId] || bv.status;
    const remarks = this.remarksByBgId[bv.verificationId] ?? bv.remarks;
    this.backgroundVerificationService.update(bv.verificationId, status, remarks).subscribe(() => {
      this.message = `Background verification #${bv.verificationId} updated.`;
      this.loadBackgroundVerifications();
    });
  }

  updateLoanVerification(lv: LoanVerification): void {
    const status = this.statusByLvId[lv.loanVerificationId] || lv.status;
    const remarks = this.remarksByLvId[lv.loanVerificationId] ?? lv.remarks;
    const result = this.resultByLvId[lv.loanVerificationId] ?? lv.verificationResult;
    this.loanVerificationService.update(lv.loanVerificationId, result, status, remarks).subscribe(() => {
      this.message = `Loan verification #${lv.loanVerificationId} updated (cascades to loan request status).`;
      this.loadLoanVerifications();
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
