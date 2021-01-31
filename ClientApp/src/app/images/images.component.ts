import { Component, Inject } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable} from 'rxjs';
import { Image, Tag } from '../image';

@Component({
  selector: 'app-albums',
  templateUrl: './images.component.html'
})
export class ImagesComponent {
  public images: Image[];
  public tags: Tag[];
  public selectedImage: Image;
  public selectedTag: Tag;
  private imagesUrl;
  private tagsUrl;
  public imageToUpload: File;
  selectedFile: ImageSnippet;
  postId;

  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };

  constructor(
    private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.imagesUrl = baseUrl + 'images/';
    this.tagsUrl = baseUrl + 'tags/';
    this.getImages();
    this.getTags();
  }

  public getImages() {
    this.http.get<Image[]>(this.imagesUrl).subscribe(result => {
      this.images = result;
    }, error => console.error(error));
  }

  public addImage(title: string, description: string, imageUrl: string, tags: string): Observable<any> {

    let tagArray = [];
    var tagStringArray = tags.split(",");
    tagStringArray.forEach(function (tagString) {
      tagArray.push({ name: tagString });
    });

    return this.http.post<Image>(this.imagesUrl, { title: title, description: description, imageUrl: imageUrl, tags: tagArray }, this.httpOptions);
  }

  deleteImage(image) {
    this.http.delete<any>(this.imagesUrl + image.id, this.httpOptions).subscribe(
      () => {
        this.getImages();
        this.getTags();
      })
  }  

  getTags() {
    this.http.get<Tag[]>(this.tagsUrl).subscribe(result => {
      this.tags = result;
    }, error => console.error(error));
  }

  selectTag(event) {
    // This could be way more efficient, just need access to the images of the selected tag
    var tag = this.tags.find(e => e.id === Number(event.target.value));
    if (tag) {
      this.images = tag.images;
    }
    else {
      this.getImages();
    }    
  } 

  onClickSubmit(data) {
      const reader = new FileReader();
      reader.addEventListener('load', (event: any) => {

        this.selectedFile = new ImageSnippet(event.target.result, this.imageToUpload);
        this.uploadImage(this.selectedFile.file).subscribe(
          (res) => {
            this.addImage(data.title, data.description, this.imageToUpload.name, data.tags).subscribe(
              () => {
                this.getImages();
                this.getTags();
              }
            )
          },
          (err) => {

          })
      });
    reader.readAsDataURL(this.imageToUpload);
  }

  onSelectFile(event) { 
    if (event.target.files && event.target.files[0]) {
      this.imageToUpload = event.target.files[0];
    }
  }

  public uploadImage(image: File) {
    const formData = new FormData();
    formData.append('image', image);
    return this.http.post(this.imagesUrl + 'UploadFile', formData);
  }  

  selectImage(image) {
    this.selectedImage = image;
  } 
}

class ImageSnippet {
  pending: boolean = false;
  status: string = 'init';

  constructor(public src: string, public file: File) { }
}

