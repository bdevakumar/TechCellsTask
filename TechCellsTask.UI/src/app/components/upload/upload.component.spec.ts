import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';

import { of } from 'rxjs/internal/observable/of';

import { DataService } from 'src/app/core';
import { UploadComponent, LoginComponent, FileInfoComponent } from '../';
import { HeaderComponent } from "src/app/core/layouts";
import { FileInfoGroupModel } from 'src/app/models';

describe('UploadComponent', () => {
  let component: UploadComponent;
  let fixture: ComponentFixture<UploadComponent>;
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
    fixture = TestBed.createComponent(UploadComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create UploadComponent', () => {
    expect(component).toBeTruthy();
  });

  it('should call methods loadFileRequirements and loadFileInfos', () => {
    const loadFileRequirementsSpy = spyOn(component, 'loadFileRequirements');
    const loadFileInfosSpy = spyOn(component, 'loadFileRequirements');
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

});
