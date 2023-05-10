import { Component, OnInit } from '@angular/core';
import { adminstrationDto } from 'src/app/Models/Administration';
import { addAdmin } from 'src/app/Models/Administration';
import { AdministrationService } from 'src/app/Services/administration.service';
import { DomSanitizer } from '@angular/platform-browser';
import { NgbModal, NgbModalRef } from '@ng-bootstrap/ng-bootstrap';
import { FormBuilder, FormGroup, NgForm, Validators } from '@angular/forms';
import { HelperService } from 'src/app/Services/helper.service';
import { NotificationService } from 'src/app/Services/notification.service';
@Component({
  selector: 'app-administration',
  templateUrl: './administration.component.html',
  styleUrls: ['./administration.component.scss']
})
export class AdministrationComponent implements OnInit{
  addAdminBody:addAdmin={
    userName: '',
    roles: 0
  };
  modalRef: NgbModalRef;
  public searchText: string;
  public p:any;
  users:adminstrationDto[]=[];
  adminsType:any[]=[];
  constructor(
    private administrationService:AdministrationService,
    private sanitizer: DomSanitizer,
    private modalService: NgbModal,
    private helper:HelperService,
    private notification:NotificationService,
    private fb: FormBuilder,
    ){}
  ngOnInit(): void {
    this.getAllModerators();
    this.getAdminsType();
  }
  getAllModerators(){
    this.administrationService.getModerators().subscribe((res)=>{
      if(res.success)
      {
        this.users=res.data;
      }
    });
  }
  getAdminsType()
  {
    this.helper.getAllAdmin().subscribe((res)=>{
      this.adminsType=res;
    });
  }
  getPictureUrl(picture: string): any {
    return this.sanitizer.bypassSecurityTrustUrl(`data:image/png;base64,${picture}`);
  }
  openModal(modalContent: any){
    this.modalRef = this.modalService.open(modalContent);
  }
  openModalDelete(modalContent: any,userId: number){
    this.modalRef = this.modalService.open(modalContent);
    this.modalRef.result.then((result) => {
      if (result === 'yes') {
        this.deleteAdmin(userId);
      }
    });
  }
  closeModal() {
    this.modalRef.close();
  }
  addAdmin(){
    debugger
    this.administrationService.addModerator(this.addAdminBody).subscribe((res)=>{
      if(res.success)
      {
        this.notification.showSuccess(res.message,"Add");        
        this.modalRef.close();
      }
      else
      {
        this.notification.showError(res.message,"Add");
      }
    });
  }
  deleteAdmin(userId: number) {
    this.administrationService.deleteModerator(userId).subscribe((res)=>{
      if(res.success)
        {
          this.notification.showSuccess(res.message,"Delete");
          this.modalRef.close();
        }
        else
        {
          this.notification.showError(res.message,"Delete");
        }
    });
  }


}
