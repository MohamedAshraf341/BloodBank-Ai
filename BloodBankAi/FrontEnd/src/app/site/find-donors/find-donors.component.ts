import { Component, OnInit } from '@angular/core';
import { FindDonorsService } from 'src/app/Services/find-donors.service';
import { getDonors } from 'src/app/Models/Donors';
import { getDonorById } from 'src/app/Models/Donors';
import { DomSanitizer } from '@angular/platform-browser';
import { NgbModal, NgbModalRef } from '@ng-bootstrap/ng-bootstrap';
@Component({
  selector: 'app-find-donors',
  templateUrl: './find-donors.component.html',
  styleUrls: ['./find-donors.component.scss']
})
export class FindDonorsComponent implements OnInit {
  donor: getDonorById;
  donors: getDonors[] = [];
  searchText: string;
  p: any;
  type: string = 'grid';
  modalRef: NgbModalRef;
  constructor(private donorService: FindDonorsService,
    private sanitizer: DomSanitizer,
    private modalService: NgbModal) { }
  ngOnInit(): void {
    this.getDonors();
  }
  getDonors() {
    this.donorService.getAllDonors().subscribe((res) => {
      if (res.success) {
        this.donors = res.data;
      }
    });
  }
  toggle(type: string) {
    this.type = type;
  }
  getPictureUrl(picture: string): any {
    return this.sanitizer.bypassSecurityTrustUrl(`data:image/png;base64,${picture}`);
  }
  getdonor(id: string, modalContent: any) {
    this.donorService.getDonorById(id).subscribe((res) => {
      if (res.success) {
        this.donor = res.data;
        this.modalRef = this.modalService.open(modalContent);

      }
    });
  }
   closeModal() {
    this.modalRef.close();
  }
}
