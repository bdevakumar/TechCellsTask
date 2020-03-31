import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { ToastrService } from 'ngx-toastr';

import { DataService } from 'src/app/core';

@Component({
  selector: 'app-upload',
  templateUrl: './upload.component.html',
  styleUrls: []
})
export class UploadComponent implements OnInit {

  public allowedFileSize: number = 524288;
  public allowedExtensions: string[] = [];
  public allowedExtensionsAsString: string = ".png";

  private url: string = 'api/FileInfo';

  @Output() public onUpload = new EventEmitter();

  constructor(
    private dataService: DataService,
    private toastr: ToastrService
  ) { }

  ngOnInit() {
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

  handleFileInput(files: FileList) {
    if (files.length > 0) {
      let file = files.item(0);
      let fileExt = file.name.split('.').pop().toLowerCase();
      if (!this.allowedExtensions.find(f => f.includes(fileExt))) {
        this.toastr.error('File type is not valid', 'Error');
      }
      else if (file.size > this.allowedFileSize) {
        this.toastr.error('File size is not valid', 'Error');
      }
      else {
        this.dataService.uploadFile(this.url + '/Upload', file)
          .subscribe((s: any) => {
            this.toastr.success('File loaded successfully', 'Successfully');
            this.onUpload.emit();
          }, error => console.log(error));
      }
    }
  }

}
