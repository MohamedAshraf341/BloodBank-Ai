import { Component, OnInit } from '@angular/core';
import { bank } from 'src/app/Models/Admin';
import { AdminService } from 'src/app/Services/admin.service';
import { DomSanitizer } from '@angular/platform-browser';
import { ActivatedRoute } from '@angular/router';
import { FormBuilder, FormGroup, NgForm, Validators } from '@angular/forms';
import { HelperService } from 'src/app/Services/helper.service';
import { NotificationService } from 'src/app/Services/notification.service';
import { addModerator } from 'src/app/Models/Admin';
import { NgbModal, NgbModalRef } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-bank-edit',
  templateUrl: './bank-edit.component.html',
  styleUrls: ['./bank-edit.component.scss']
})
export class BankEditComponent implements OnInit {
  modalRef: NgbModalRef;
  governs:any[]=[];
  cities:any[]=[];
  moderators:any[]=[];
  bankModerator:addModerator={
    bankId: 0,
    userName: '',
    roles: 0
  };
  bank:bank;
  bankId:any;
  bankUpdateForm: FormGroup;
  constructor(
    private modalService: NgbModal,
    private helper:HelperService,
    private notification:NotificationService,
    private fb: FormBuilder,
    private adminService:AdminService,
    private sanitizer: DomSanitizer,
    private route: ActivatedRoute,
    ){}
  ngOnInit(): void {
    this.bankId=this.route.snapshot.paramMap.get('id');
    // this.route.params.subscribe((params) => {
    //   this.getBank(params['id']);
    // });
    this.getBank(this.bankId);
    this.getGoverns();
    this.getCities();
    this.getModerators();
  }
  getBank(id:number)
  {
    this.adminService.getBank(id).subscribe((res)=>{
      if(res.success)
      {
        this.bank=res.data;
        this.initilizeForm();
      }
    })
  }
  getPictureUrl(picture: string): any {
    return this.sanitizer.bypassSecurityTrustUrl(`data:image/png;base64,${picture}`);
  }
  initilizeForm() {
    this.bankUpdateForm = this.fb.group({
      id: [this.bank.id],
      name: [this.bank.name, Validators.required],
      website: [this.bank.website],
      phoneNumber: [this.bank.phoneNumber, Validators.required],
      email: [this.bank.email, [Validators.email, Validators.required]],
      area: [this.bank.address.area, Validators.required],
      city: [this.bank.address.city, Validators.required],
      government: [this.bank.address.government, Validators.required],
    });
  }
  updateBank(bankId:number) {
    this.adminService
      .updateBank(this.bankUpdateForm.value,bankId)
      .subscribe((res) => {
        if(res.success)
        {
          this.notification.showSuccess(res.message,"Update");
        }
        else
        {
          this.notification.showError(res.message,"Update");
        }
      });
  }
  getGoverns()
  {
    this.helper.getAllGov().subscribe((res)=>{
      this.governs=res;
    });
  }
  getCities()
  {
    this.helper.getAllCit().subscribe((res)=>{
      this.cities=res;
    });
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
    this.bankModerator.bankId=this.bank.id;
    this.adminService.addModerator(this.bankModerator).subscribe((res)=>{
      if(res.success)
      {
        this.notification.showSuccess(res.message,"Add");        
        this.getBank(this.bankId);
        this.modalRef.close();
      }
      else
      {
        this.notification.showError(res.message,"Add");
      }
    });
  }
  deleteUser(userId: number) {
    this.adminService.deleteModerator(userId).subscribe((res)=>{
      if(res.success)
        {
          this.notification.showSuccess(res.message,"Delete");
          this.getBank(this.bankId);
          this.modalRef.close();
        }
        else
        {
          this.notification.showError(res.message,"Delete");
        }
    });
  }
}
