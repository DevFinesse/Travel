import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { TourService, Tour } from '../../../core/services/tour.service';

@Component({
    selector: 'app-tour-list',
    standalone: true,
    imports: [CommonModule, RouterModule],
    templateUrl: './tour-list.component.html',
    styleUrl: './tour-list.component.css'
})
export class TourListComponent implements OnInit {
    tours: Tour[] = [];

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

    deleteTour(id: number) {
        if (confirm('Are you sure you want to delete this tour?')) {
            this.tourService.deleteTour(id).subscribe({
                next: () => {
                    this.tours = this.tours.filter(t => t.id !== id);
                },
                error: (err) => console.error('Error deleting tour', err)
            });
        }
    }
}
