import { Component, OnInit } from '@angular/core';
import { AcountService } from 'src/app/Services/acount.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-second-nav',
  templateUrl: './second-nav.component.html',
  styleUrls: ['./second-nav.component.scss']
})
export class SecondNavComponent implements OnInit {
  isAuthenticated = false;
  isAdmin = false;
  isModerator = false;
  isBankAdmin = false;
  isBankModerator = false;
  isMember = false;
  constructor(public authService: AcountService) { }

  ngOnInit(): void {
    this.isAuthenticated = this.authService.isAuthenticated();
    if (this.isAuthenticated) {
      const user = this.authService.getUser();
      this.isAdmin = user.roles.includes('Admin');
      this.isModerator = user.roles.includes('Moderator');
      this.isBankAdmin = user.roles.includes('BankAdmin');
      this.isBankModerator = user.roles.includes('BankModerator');
      this.isMember = user.roles.includes('Member');
    }
  }
}
