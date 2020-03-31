import { FormsModule } from '@angular/forms';
import { TestBed, async } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';

import { AppComponent } from './app.component';
import { 
  LoginComponent, 
  FileInfoComponent, 
  UploadComponent 
} from './components';
import { DataService } from './core/services';
import { HeaderComponent } from './core/layouts';

describe('AppComponent', () => {
  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [
        FormsModule,
        RouterTestingModule
      ],
      declarations: [
        AppComponent,
        LoginComponent,
        HeaderComponent,
        UploadComponent,
        FileInfoComponent
      ],
      providers: [
        DataService
      ]
    }).compileComponents();
  }));

  it('should create the app', () => {
    const fixture = TestBed.createComponent(AppComponent);
    const app = fixture.debugElement.componentInstance;
    expect(app).toBeTruthy();
  });
});
