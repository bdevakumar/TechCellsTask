import { Component, OnInit } from '@angular/core';

import { DataService } from 'src/app/core';
import { FileInfoGroupModel } from 'src/app/models';

import { environment } from "src/environments/environment";

@Component({
  selector: 'app-file-info',
  templateUrl: './file-info.component.html',
  styleUrls: []
})
export class FileInfoComponent implements OnInit {

  public fileInfoGroups: FileInfoGroupModel[] = [];

  private url: string = 'api/FileInfo';

  constructor(private dataService: DataService) { }

  ngOnInit() {
    this.loadFileInfos();
  }

  loadFileInfos() {
    this.dataService.get<FileInfoGroupModel[]>(this.url + '/GetFileInfos')
      .subscribe(s => {
        this.fileInfoGroups = s;
      });
  }

  update(data) {
    if (data) {
      this.loadFileInfos()
    }
  }

  download(link, name) {
    fetch(environment.serverUrl + '/' + link)
      .then(resp => resp.blob())
      .then(blob => {
        const url = window.URL.createObjectURL(blob);
        const a = document.createElement('a');
        a.style.display = 'none';
        a.href = url;
        a.download = name;
        document.body.appendChild(a);
        a.click();
        window.URL.revokeObjectURL(url);
      }).catch(() => console.log('The file not found'));
  }

}
