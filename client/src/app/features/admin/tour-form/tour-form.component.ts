import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { TourService } from '../../../core/services/tour.service';

@Component({
    selector: 'app-tour-form',
    standalone: true,
    imports: [CommonModule, ReactiveFormsModule, RouterModule],
    templateUrl: './tour-form.component.html',
    styleUrl: './tour-form.component.css'
})
export class TourFormComponent implements OnInit {
    tourForm: FormGroup;
    isEditMode = false;
    tourId: number | null = null;

    constructor(
        private fb: FormBuilder,
        private tourService: TourService,
        private route: ActivatedRoute,
        private router: Router
    ) {
        this.tourForm = this.fb.group({
            name: ['', Validators.required],
            description: [''],
            location: ['', Validators.required],
            price: [0, [Validators.required, Validators.min(0)]],
            imageUrl: [''],
            startDate: ['', Validators.required],
            endDate: ['', Validators.required],
            availableSlots: [0, [Validators.required, Validators.min(1)]]
        });
    }

    ngOnInit(): void {
        this.route.params.subscribe(params => {
            if (params['id']) {
                this.isEditMode = true;
                this.tourId = +params['id'];
                this.loadTour(this.tourId);
            }
        });
    }

    loadTour(id: number) {
        this.tourService.getTourById(id).subscribe({
            next: (tour) => {
                this.tourForm.patchValue({
                    ...tour,
                    startDate: new Date(tour.startDate).toISOString().split('T')[0],
                    endDate: new Date(tour.endDate).toISOString().split('T')[0]
                });
            },
            error: (err) => console.error('Error loading tour', err)
        });
    }

    onSubmit() {
        if (this.tourForm.valid) {
            const tourData = this.tourForm.value;
            if (this.isEditMode && this.tourId) {
                this.tourService.updateTour(this.tourId, tourData).subscribe({
                    next: () => this.router.navigate(['/admin/tours']),
                    error: (err) => console.error('Error updating tour', err)
                });
            } else {
                this.tourService.createTour(tourData).subscribe({
                    next: () => this.router.navigate(['/admin/tours']),
                    error: (err) => console.error('Error creating tour', err)
                });
            }
        }
    }
}
