import { Component, OnInit } from '@angular/core';
import { DomSanitizer } from '@angular/platform-browser';
import { NgbModal, NgbModalRef } from '@ng-bootstrap/ng-bootstrap';
import { FindBanksService } from 'src/app/Services/find-banks.service';
import{bankWithBloodGroups}from 'src/app/Models/Banks';
import{bankByIdWithAddress}from 'src/app/Models/Banks';
@Component({
  selector: 'app-find-banks',
  templateUrl: './find-banks.component.html',
  styleUrls: ['./find-banks.component.scss']
})
export class FindBanksComponent implements OnInit {
  banks:bankWithBloodGroups[]=[];
  bank:bankByIdWithAddress;
  public modalRef: NgbModalRef;
  public searchText: string;
  public p:any;
  public type:string = 'grid';
  public toggle(type:string){
    this.type = type;
  }
  constructor(
    private sanitizer: DomSanitizer,
    public modalService: NgbModal,
    private bankService:FindBanksService
  ){}
  ngOnInit(): void {
    this.getBanks();
  }
  getBanks()
  {
    this.bankService.getAllBanks().subscribe((res)=>{
      if(res.success)
      {
        this.banks=res.data;
      }
    });
  }
  getBankById(id:number,modalContent:any)
  {
    this.bankService.getBankById(id).subscribe((res)=>{
      if(res.success)
      {
        this.bank=res.data;
        this.modalRef = this.modalService.open(modalContent);

      }
    });
  }
  public closeModal(){
    this.modalRef.close();
  }
  getPictureUrl(picture: string): any {
    return this.sanitizer.bypassSecurityTrustUrl(`data:image/png;base64,${picture}`);
  }
}
