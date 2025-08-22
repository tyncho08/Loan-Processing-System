import { Component, OnInit } from '@angular/core';
import { LoanService } from '../loan.service';
import { UserService } from '../user.service';

@Component({
  selector: 'app-viewloan',
  templateUrl: './viewloan.component.html',
  styleUrls: ['./viewloan.component.css']
})
export class ViewloanComponent implements OnInit {

  loans = [];
  loading = true;
  error = '';
  constructor(public loanService: LoanService, public userService: UserService) { }

  ngOnInit() {
    //retrieves list of loans
    this.loading = true;
    this.error = '';
    this.loanService.getAllLoans().subscribe(resp => {
      // console.log('ViewLoan data received:', resp);
      this.loans = resp || [];
      this.loading = false;
    }, err => {
      console.log(err);
      if (err.status === 0) {
        this.error = 'Cannot connect to server. Please make sure the backend is running on port 5275.';
      } else {
        this.error = `Error loading loan data: ${err.message || 'Unknown error'}`;
      }
      this.loading = false;
      // Don't show alert, instead show error message in the UI
    }, () => {
      console.log("loan details retrieved successfully");
    }
    )
  }

  retryLoad() {
    this.ngOnInit();
  }
}
