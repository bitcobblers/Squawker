import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { HomeFeedService, Post } from '../home-feed.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent {

  public feed: Post[] = [];

  constructor(feed: HomeFeedService) {
    feed.load().subscribe(posts => {
      this.feed = posts;
    })
  }
}
