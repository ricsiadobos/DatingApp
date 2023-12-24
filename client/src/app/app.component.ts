import {Component, OnInit} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {ConsoleLogger} from "@angular/compiler-cli";
import {Observable} from "rxjs";
import { User } from './_models/user';
import { AccountService } from './_services/account.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})

export class AppComponent implements OnInit{
  title = 'client';
  users: any;
  public myUsers: Array<User> = [];

  constructor(private accountService: AccountService) {}

  ngOnInit() {

    this.setCurrentUser();

  }

  setCurrentUser() {
    //user-nek átadjuk a localStorge-ban lévő elemet, aminek 'user' a key. 
    // ezt átadjuk a accountService-nek
    const user: User = JSON.parse(localStorage.getItem('user'));
    this.accountService.setCurrentUser(user);
  }

 
 }
