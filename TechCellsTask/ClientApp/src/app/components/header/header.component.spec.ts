import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';

import { AppRoutingModule } from 'src/app/app-routing.module';
import { HeaderComponent, UploadComponent, LoginComponent } from '../';

describe('HeaderComponent', () => {
  let component: HeaderComponent;
  let fixture: ComponentFixture<HeaderComponent>;
  let router: Router;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [AppRoutingModule, FormsModule],
      declarations: [HeaderComponent, UploadComponent, LoginComponent],
      providers: []
    }).compileComponents();
  }));

  beforeEach(() => {
    router = TestBed.get(Router);
    fixture = TestBed.createComponent(HeaderComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create HeaderComponent', () => {
    expect(component).toBeTruthy();
  });

  it('should set isAuthorized to false', () => {
    component.ngOnInit();
    expect(component.isAuthorized).toBeFalsy();
  });

  it('should navigate to login page after signout called', () => {
    const navigateSpy = spyOn(router, 'navigate');
    component.signout();
    expect(navigateSpy).toHaveBeenCalledWith(['/login'], { replaceUrl: true });
  });
});
