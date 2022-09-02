import {Component, OnInit} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {ConsoleLogger} from "@angular/compiler-cli";
import {Observable} from "rxjs";
import { User } from './User';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})

export class AppComponent implements OnInit{
  title = 'client';
  users: any;
  myUsers: Array<User> = [];

  constructor(private http: HttpClient) {}

  ngOnInit() {
    this.getUsers();
    
    this.getUsers2().subscribe(data => {
      this.myUsers = data;
    });
    
  }

 
 
  getUsers() {
    this.http.get('https://localhost:7145/api/users').subscribe(response =>{
      this.users = response;
    }, error => {
      console.log(error);
    })
  }

  getUsers2(): Observable<any>{
    return this.http.get<any>('https://localhost:7145/api/users')
  }

}
