import { Component, OnInit } from '@angular/core';
import { HttpHeaders } from '@angular/common/http';
import { DataService } from 'src/app/core';
import { FileInfoGroupModel } from 'src/app/models';

@Component({
  selector: 'app-upload',
  templateUrl: './upload.component.html',
  styleUrls: ['./upload.component.css']
})
export class UploadComponent implements OnInit {

  private url: string = 'api/FileInfo';
  public fileInfoGroups: FileInfoGroupModel[] = [];
  public allowedExtensions: string[] = [];
  public allowedExtensionsAsString: string = ".png";
  public allowedFileSize: number = 524288;

  constructor(private dataService: DataService) { }

  ngOnInit() {
    this.loadFileInfos();
    this.loadFileRequirements();
  }

  loadFileRequirements() {
    this.dataService.get<any>(this.url + '/GetFileRequirements')
      .subscribe(s => {
        this.allowedExtensions = s.allowedExtensions;
        this.allowedFileSize = s.allowedFileSize;
        this.allowedExtensionsAsString = this.allowedExtensions.join(',');
      })
  }

  loadFileInfos() {
    this.dataService.get<FileInfoGroupModel[]>(this.url + '/GetFileInfos')
      .subscribe(s => {
        this.fileInfoGroups = s;
      });
  }

  handleFileInput(files: FileList) {
    if (files.length > 0) {
      let file = files.item(0);
      let fileExt = file.name.split('.').pop().toLowerCase();
      if (!this.allowedExtensions.find(f => f.includes(fileExt))) {
        alert('File type is not valid');
      }
      else if (file.size > this.allowedFileSize) {
        alert('File size is not valid');
      }
      else {
        this.dataService.uploadFile(this.url + '/Upload', file)
          .subscribe((s: any) => {
            alert('File loaded successfully');
            this.loadFileInfos();
          }, error => console.log(error));
      }
    }
  }

}
