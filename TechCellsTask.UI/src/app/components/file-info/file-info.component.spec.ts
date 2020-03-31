import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';

import { of } from 'rxjs/internal/observable/of';

import { DataService } from 'src/app/core';
import { UploadComponent, LoginComponent, FileInfoComponent } from '../';
import { HeaderComponent } from "src/app/core/layouts";
import { FileInfoGroupModel } from 'src/app/models';

describe('FileInfoComponent', () => {
  let component: FileInfoComponent;
  let fixture: ComponentFixture<FileInfoComponent>;
  let dataService: DataService;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [FormsModule, HttpClientModule, RouterTestingModule],
      declarations: [UploadComponent, LoginComponent, HeaderComponent, FileInfoComponent],
      providers: [DataService]
    }).compileComponents();
  }));

  beforeEach(() => {
    dataService = TestBed.get(DataService);
    fixture = TestBed.createComponent(FileInfoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create FileInfoComponent', () => {
    expect(component).toBeTruthy();
  });

  it("should call loadFileInfos and return list of fileinfos", async(() => {
    const response: FileInfoGroupModel[] = [];
    spyOn(dataService, 'get').and.returnValue(of(response))
    component.loadFileInfos();
    fixture.detectChanges();
    expect(component.fileInfoGroups).toEqual(response);
  }));
});
