import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AboutUsComponent } from './site/about-us/about-us.component';
import { ContactComponent } from './site/contact/contact.component';
import { DonationProcessDesComponent } from './site/donation-process-des/donation-process-des.component';
import { DonationProcessComponent } from './site/donation-process/donation-process.component';
import { FristTimeDonationComponent } from './site/frist-time-donation/frist-time-donation.component';
import { HomeComponent } from './site/home/home.component';
import { SelectTimeTodonationComponent } from './site/select-time-todonation/select-time-todonation.component';
import { WhatHappenDonatebloodComponent } from './site/what-happen-donateblood/what-happen-donateblood.component';
import { FindDonorsComponent } from './site/find-donors/find-donors.component';
import { FindBanksComponent } from './site/find-banks/find-banks.component';
import { AdminPageComponent } from './site/Admin/admin-page/admin-page.component';
import { BankRegisterComponent } from './site/Admin/bank-register/bank-register.component';
import { BankEditComponent } from './site/Admin/bank-edit/bank-edit.component';
import { AdministrationComponent } from './site/administration/administration.component';
import { LoginComponent } from './Auth/login/login.component';
import { SignUpComponent } from './Auth/sign-up/sign-up.component';
import { EditProfileComponent } from './Auth/edit-profile/edit-profile.component';
import { AuthGuard } from './Guard/auth.guard';
import { RoleGuard } from './Guard/role.guard';
import { ModeratorPageComponent } from './site/Moderator/moderator-page/moderator-page.component';
import { EditeBloodComponent } from './site/Moderator/edite-blood/edite-blood.component';
const routes: Routes = [
  {path: 'home', component: HomeComponent },
  {path:'about',component:AboutUsComponent},
  {path:'contact',component:ContactComponent},
  {path:'donation-process',component:DonationProcessComponent},
  {path:'donation_process_des',component: DonationProcessDesComponent },
  {path:'what-happen-donateblood',component:WhatHappenDonatebloodComponent},
  {path:"frist-time-donation",component:FristTimeDonationComponent},
  {path:'select-time-todonation',component:SelectTimeTodonationComponent},

  {path:'find-donors',component:FindDonorsComponent ,canActivate: [AuthGuard, RoleGuard]},
  {path:'find-banks',component:FindBanksComponent,canActivate: [AuthGuard, RoleGuard]},
  {path:'administration',component:AdministrationComponent,canActivate: [AuthGuard, RoleGuard]},
  {path:'Login',component:LoginComponent},
  {path:'SignUp',component:SignUpComponent},
  {path:'EditProfile',component:EditProfileComponent,canActivate: [AuthGuard, RoleGuard]},
  {path:'moderator-page'
  ,children:[
         { path: 'bank/:id', component: EditeBloodComponent},
         { path: '', component: ModeratorPageComponent },
  ],canActivate: [AuthGuard, RoleGuard]}, 
  {path:'admin-page'
   ,children:[
    { path: 'bank-register', component: BankRegisterComponent },
          { path: 'bank/:id', component: BankEditComponent},
          { path: '', component: AdminPageComponent },
   ],canActivate: [AuthGuard, RoleGuard]},  
  { path: '', redirectTo: 'home', pathMatch: 'full' },
  { path: '**', component: HomeComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
