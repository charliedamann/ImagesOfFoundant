//import { HttpClient, HttpHeaders } from '@angular/common/http';
//import { Observable, of } from 'rxjs';
//import { Image } from './image';
//export class ImageService {
//  private imagesUrl = 'api/heroes';  // URL to web api
//  httpOptions = {
//    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
//  };
//  constructor(
//    private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
//    this.imagesUrl = baseUrl + 'images';
//    http.get<FoundantImage[]>(baseUrl + 'images').subscribe(result => {
//      this.images = result;
//    }, error => console.error(error));
//  }
//  addImage(title: string, description: string) {
//    this.http.post<Image>(this.imagesUrl, { title: title, description: description }, this.httpOptions).subscribe(
//      data => {
//        //this.postId = data.id;
//      })
//  }
//}
//# sourceMappingURL=image.service.js.map