import { Observable } from 'rxjs'
import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class HomeFeedService {

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {    
  }

  public load() : Observable<Post[]>{
    return this.http.get<Post[]>(this.baseUrl + 'api/post/')
  }
}

export class Post {
  public content: string | undefined;

}
