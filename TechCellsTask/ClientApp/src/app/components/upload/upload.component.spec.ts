import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';

import { UploadComponent, LoginComponent, HeaderComponent } from '../';
import { DataService } from 'src/app/core';
import { of } from 'rxjs/internal/observable/of';
import { FileInfoGroupModel } from 'src/app/models';

describe('UploadComponent', () => {
  let component: UploadComponent;
  let fixture: ComponentFixture<UploadComponent>;
  let dataService: DataService;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [FormsModule, HttpClientModule, RouterTestingModule],
      declarations: [UploadComponent, LoginComponent, HeaderComponent],
      providers: [DataService]
    }).compileComponents();
  }));

  beforeEach(() => {
    dataService = TestBed.get(DataService);
    fixture = TestBed.createComponent(UploadComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create UploadComponent', () => {
    expect(component).toBeTruthy();
  });

  it('should call methods loadFileRequirements and loadFileInfos', () => {
    const loadFileRequirementsSpy = spyOn(component, 'loadFileRequirements');
    const loadFileInfosSpy = spyOn(component, 'loadFileInfos');
    component.ngOnInit();
    expect(loadFileRequirementsSpy).toHaveBeenCalled();
    expect(loadFileInfosSpy).toHaveBeenCalled();
  });

  it('should set file requirement fields', () => {
    component.loadFileRequirements();
    fixture.detectChanges();
    expect(component.allowedExtensions).toEqual([]);
    expect(component.allowedFileSize).toEqual(524288);
  });

  it("should call loadFileInfos and return list of fileinfos", async(() => {
    const response: FileInfoGroupModel[] = [];
    spyOn(dataService, 'get').and.returnValue(of(response))
    component.loadFileInfos();
    fixture.detectChanges();
    console.log('response');
    console.log(response);
    expect(component.fileInfoGroups).toEqual(response);
  }));
});
