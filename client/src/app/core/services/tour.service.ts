import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';

export interface Tour {
    id: number;
    name: string;
    description: string;
    location: string;
    price: number;
    imageUrl: string;
    startDate: string;
    endDate: string;
    availableSlots: number;
    isFeatured: boolean;
}

export interface CreateTourDto {
    name: string;
    description: string;
    location: string;
    price: number;
    imageUrl: string;
    startDate: string;
    endDate: string;
    availableSlots: number;
    isFeatured: boolean;
}

@Injectable({
    providedIn: 'root'
})
export class TourService {
    private apiUrl = `${environment.apiUrl}/tours`;

    constructor(private http: HttpClient) { }

    getAllTours(): Observable<Tour[]> {
        return this.http.get<Tour[]>(this.apiUrl);
    }

    getTourById(id: number): Observable<Tour> {
        return this.http.get<Tour>(`${this.apiUrl}/${id}`);
    }

    createTour(tour: CreateTourDto): Observable<Tour> {
        return this.http.post<Tour>(this.apiUrl, tour);
    }

    updateTour(id: number, tour: CreateTourDto): Observable<void> {
        return this.http.put<void>(`${this.apiUrl}/${id}`, tour);
    }

    deleteTour(id: number): Observable<void> {
        return this.http.delete<void>(`${this.apiUrl}/${id}`);
    }

    searchTours(location?: string, minPrice?: number, maxPrice?: number, startDate?: string): Observable<Tour[]> {
        let params = new HttpParams();
        if (location) params = params.set('location', location);
        if (minPrice) params = params.set('minPrice', minPrice);
        if (maxPrice) params = params.set('maxPrice', maxPrice);
        if (startDate) params = params.set('startDate', startDate);

        return this.http.get<Tour[]>(`${this.apiUrl}/search`, { params });
    }

    getFeaturedTours(): Observable<Tour[]> {
        return this.http.get<Tour[]>(`${this.apiUrl}/featured`);
    }

    toggleFeatured(id: number): Observable<void> {
        return this.http.put<void>(`${this.apiUrl}/${id}/toggle-featured`, {});
    }
}
