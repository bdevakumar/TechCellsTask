import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { Router, ActivatedRoute } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';

import { HeaderComponent, LoginComponent, UploadComponent } from '../';
import { DataService } from 'src/app/core';

describe('LoginComponent', () => {
  let component: LoginComponent;
  let fixture: ComponentFixture<LoginComponent>;
  let router: Router;
  let activatedRoute: ActivatedRoute;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [RouterTestingModule, FormsModule, HttpClientModule],
      declarations: [LoginComponent, HeaderComponent, UploadComponent],
      providers: [DataService]
    }).compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(LoginComponent);
    router = TestBed.get(Router);
    router.initialNavigation();
    activatedRoute = TestBed.get(ActivatedRoute);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create LoginComponent', () => {
    expect(component).toBeTruthy();
  });

  it('should set returnUrl to null', () => {
    component.ngOnInit();
    expect(component.returnUrl).toBeNull();
  });

  it('should call alert with message "Please enter the username"', () => {
    spyOn(window, 'alert');
    component.signIn();
    expect(window.alert).toHaveBeenCalledWith('Please enter the username');
  });

  it('should call home page', () => {
    const navigateSpy = spyOn(router, 'navigate');
    component.returnUrl = '/';
    component.redirect();
    expect(navigateSpy).toHaveBeenCalledWith(['/'], { replaceUrl: true });
  });
});
