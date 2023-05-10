import { Component, OnInit } from '@angular/core';
import { banks } from 'src/app/Models/Admin';
import { AdminService } from 'src/app/Services/admin.service';
import { DomSanitizer } from '@angular/platform-browser';

@Component({
  selector: 'app-admin-page',
  templateUrl: './admin-page.component.html',
  styleUrls: ['./admin-page.component.scss']
})
export class AdminPageComponent implements OnInit {
  constructor(private adminService:AdminService,
    private sanitizer: DomSanitizer,){}
  ngOnInit(): void {
    this.getAllBanks();
  }  
  public searchText: string;
  public p:any;
  banks:banks[]=[];
  getAllBanks()
  {
    this.adminService.getAllBanks().subscribe((res)=>{
      if(res.success)
      {
        this.banks=res.data;
      }
    })
  }
  getPictureUrl(picture: string): any {
    return this.sanitizer.bypassSecurityTrustUrl(`data:image/png;base64,${picture}`);
  }
}
