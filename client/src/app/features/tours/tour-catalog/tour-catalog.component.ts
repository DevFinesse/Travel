import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { TourService, Tour } from '../../../core/services/tour.service';

@Component({
    selector: 'app-tour-catalog',
    standalone: true,
    imports: [CommonModule, RouterModule, FormsModule],
    templateUrl: './tour-catalog.component.html',
    styleUrl: './tour-catalog.component.css'
})
export class TourCatalogComponent implements OnInit {
    tours: Tour[] = [];
    location: string = '';
    minPrice: number | null = null;
    maxPrice: number | null = null;
    startDate: string = '';

    constructor(private tourService: TourService) { }

    ngOnInit(): void {
        this.loadTours();
    }

    loadTours() {
        this.tourService.getAllTours().subscribe({
            next: (data) => this.tours = data,
            error: (err) => console.error('Error loading tours', err)
        });
    }

    onSearch() {
        this.tourService.searchTours(this.location, this.minPrice || undefined, this.maxPrice || undefined, this.startDate || undefined)
            .subscribe({
                next: (data) => this.tours = data,
                error: (err) => console.error('Error searching tours', err)
            });
    }
}
