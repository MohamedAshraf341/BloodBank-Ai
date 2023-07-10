import { Component, OnInit } from '@angular/core';
import { AcountService } from 'src/app/Services/acount.service';
import { Router } from '@angular/router';
import { NotificationService } from 'src/app/Services/notification.service';
import { responseAuth } from 'src/app/Models/Acount';
import { RevokeTokenDto } from 'src/app/Models/Acount';
import { DomSanitizer } from '@angular/platform-browser';


@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.scss']
})
export class NavBarComponent implements OnInit {
  isAuthenticated = false;
  username = '';
  name = '';
  profilePicture = '';
  constructor(
    private notification: NotificationService,
    public authService: AcountService,
    public router: Router,
    private sanitizer: DomSanitizer,

  ) { 
  }

  ngOnInit(): void {
    this.isAuthenticated = this.authService.isAuthenticated();
    if (this.isAuthenticated) {
      const user = this.authService.getUser();
      this.username = user.username;
      this.name=user.name;
      this.profilePicture = user.picture;
    }
  }
  onLogout(): void {
    this.authService.clearToken();
    this.authService.clearUser();
    this.router.navigate(['/'])
          .then(() => {
            
            window.location.reload();
            this.notification.showSuccess("Log Out Successfully", "Login")

          });
  }

  onLogin(): void {
    this.router.navigate(['/Login']);
  }

  onRegister(): void {
    this.router.navigate(['/SignUp']);
  }


  getPicture(pic:any): any {
    return this.sanitizer.bypassSecurityTrustUrl(`data:image/png;base64,${pic}`);
  }



}
