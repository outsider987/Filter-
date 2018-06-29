import { Component, ViewChild } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { NgModel } from '@angular/forms';
import {debounceTime} from 'rxjs/operators';
import { combineLatest } from 'rxjs';



@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'app';
  data: any;
  keyword = '';
  Zone = '';
  Infor = '';
  @ViewChild('tKeyword') tkeyword: NgModel;
  @ViewChild('tZone') tZone: NgModel;
  @ViewChild('tInfor') tInfor: NgModel;

  constructor(private http: HttpClient) { }
   // tslint:disable-next-line:use-life-cycle-interface
  ngOnInit(): void {
    this.http.get('/api/spots').subscribe((value: any) => {
      this.data = value;
    });
  }
  // tslint:disable-next-line:use-life-cycle-interface
  ngAfterViewInit() {

    combineLatest(
      this.tkeyword.valueChanges,
      this.tZone.valueChanges,
      this.tInfor.valueChanges
     )
     .pipe(
       debounceTime(500)
      )
      .subscribe(evt => {
      const [k, z, y] = evt;
      this.http.get('api/spots?k=' + k + '&z=' + z + '&y=' + y).subscribe((value: any) => {
         this.data = value;
      });
     });



    // Called after ngAfterContentInit when the component's view has been initialized. Applies to components only.
    // Add 'implements AfterViewInit' to the class.

  }
}
