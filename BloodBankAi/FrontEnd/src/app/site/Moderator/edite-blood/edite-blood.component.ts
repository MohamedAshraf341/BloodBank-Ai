import { Component,OnInit } from '@angular/core';
import { NotificationService } from 'src/app/Services/notification.service';
import { ModeratorService } from 'src/app/Services/moderator.service';
import { FormBuilder } from '@angular/forms';
import { DomSanitizer } from '@angular/platform-browser';
import { ActivatedRoute } from '@angular/router';
import { BloodGroupUpdateDto } from 'src/app/Models/Moderator';
import { addModerator, bank } from 'src/app/Models/Admin';
import { NgbModal, NgbModalRef } from '@ng-bootstrap/ng-bootstrap';
import { HelperService } from 'src/app/Services/helper.service';

@Component({
  selector: 'app-edite-blood',
  templateUrl: './edite-blood.component.html',
  styleUrls: ['./edite-blood.component.scss']
})
export class EditeBloodComponent implements OnInit{
  bankModerator:addModerator={
    bankId: 0,
    userName: '',
    roles: 0
  };
  modalRef: NgbModalRef;
  moderators:any[]=[];
  bankId:any;
  bloodGroup:BloodGroupUpdateDto;
  bank:bank;
  removePicture = false;
  picture: any;
  constructor(
    private helper:HelperService,
    private modalService: NgbModal,
    private notification:NotificationService,
    private fb: FormBuilder,
    private moderatorService:ModeratorService,
    private sanitizer: DomSanitizer,
    private route: ActivatedRoute,
    ){}
ngOnInit(): void {
  debugger
     this.bankId=this.route.snapshot.paramMap.get('id');
     this.getBloodGroup(this.bankId);
     this.getBank(this.bankId);
}

getBloodGroup(id:number)
{
  this.moderatorService.getBloodData(id).subscribe((res)=>{
    if(res.success)
    {
      this.bloodGroup=res.data;
    }
  });
}
getBank(id:number)
{
  this.moderatorService.getBank(id).subscribe((res)=>{
    if(res.success)
    {
      this.bank=res.data;
    }
  });
}
handleFileInput(files: FileList) {
  if (files.length > 0 && this.bankId != null) {
    this.removePicture=false;
    this.picture=files.item(0);
    this.moderatorService.updateBankImage(this.bankId,this.picture,this.removePicture).subscribe((res)=>{
      if(res.success)
      {
        this.notification.showSuccess(res.message,'Change Image')
      }
      else
      {
        this.notification.showError(res.message,'Change Image')

      }
    })

  }
}
removePhoto()
{
  if (this.bankId != null)
  {
    this.removePicture=true;
    this.picture=null;
    this.moderatorService.updateBankImage(this.bankId,this.picture,this.removePicture).subscribe((res)=>{
      if(res.success)
      {
        this.notification.showSuccess(res.message,'Remove Image')
      }
      else
      {
        this.notification.showError(res.message,'Remove Image')

      }
    })
  }

}

getModerators()
{
  this.helper.getAllModerator().subscribe((res)=>{
    this.moderators=res;
  });
}
openModal(modalContent: any){
  this.modalRef = this.modalService.open(modalContent);
}
openModalDelete(modalContent: any,userId: number){
  this.modalRef = this.modalService.open(modalContent);
  this.modalRef.result.then((result) => {
    if (result === 'yes') {
      this.deleteUser(userId);
    }
  });
}
closeModal() {
  this.modalRef.close();
}
addModerator(){
  debugger
  this.bankModerator.bankId=this.bankId;
  this.moderatorService.addModerator(this.bankModerator).subscribe((res)=>{
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
deleteUser(userId: number) {
  this.moderatorService.deleteModerator(userId).subscribe((res)=>{
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
