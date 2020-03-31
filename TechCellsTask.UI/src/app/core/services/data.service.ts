import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

import { Observable } from 'rxjs';

import { RequestObserve, ResponseType } from '../../models';
import { environment } from 'src/environments/environment';

@Injectable()
export class DataService {

    constructor(public http: HttpClient) {
    }

    public get<T>(url: string, params?: any, headers?: HttpHeaders): Observable<T> {
        let slash = '';
        if (!url.startsWith('/')) {
            slash = '/';
        }
        let requestUrl = `${environment.serverUrl}${slash}${url}`;
        return this.http.get<T>(requestUrl,
            { headers: headers, params: params, observe: RequestObserve.Body, responseType: ResponseType.Json });
    }

    public post<T>(url: string, body: any, params?: any, headers?: HttpHeaders): Observable<T> {
        let slash = '';
        if (!url.startsWith('/')) {
            slash = '/';
        }
        let requestUrl = `${environment.serverUrl}${slash}${url}`;
        return this.http.post<T>(requestUrl, body,
            { headers: headers, params: params, observe: RequestObserve.Body, responseType: ResponseType.Json });
    }

    public put<T>(url: string, body: any, params?: any, headers?: HttpHeaders): Observable<T> {
        let slash = '';
        if (!url.startsWith('/')) {
            slash = '/';
        }
        let requestUrl = `${environment.serverUrl}${slash}${url}`;
        return this.http.put<T>(requestUrl, body,
            { headers: headers, params: params, observe: RequestObserve.Body, responseType: ResponseType.Json });
    }

    public delete<T>(url: string, params?: any, headers?: HttpHeaders): Observable<T> {
        let slash = '';
        if (!url.startsWith('/')) {
            slash = '/';
        }
        let requestUrl = `${environment.serverUrl}${slash}${url}`;
        return this.http.delete<T>(requestUrl,
            { headers: headers, params: params, observe: RequestObserve.Body, responseType: ResponseType.Json });
    }

    public uploadFile<T>(url: string, file: File) {
        if (!url.startsWith('/')) {
            url = '/' + url;
        }

        let formData: FormData = new FormData();
        formData.append('uploadFile', file, file.name);

        let headers = new HttpHeaders();
        headers.append('Accept', 'application/json');
        headers.append('Content-Type', 'multipart/form-data');

        let requestUrl = `${environment.serverUrl}${url}`;
        return this.http.post(requestUrl, formData, { headers: headers });
    }

}
