import { CommonModule } from '@angular/common';
import { Component, computed, input, output } from '@angular/core';
import { Product } from 'app/products/data-access/product.model';
import { ChipModule } from 'primeng/chip';
import { ButtonModule } from 'primeng/button';

@Component({
  selector: 'basket-product',
  standalone: true,
  imports: [CommonModule, ChipModule,ButtonModule],
  templateUrl: './product.component.html',
  styleUrl: './product.component.css'
})
export class ProductComponent {
product = input.required<Product>();
remove = output<void>();

  onRemoveClick()
  {
    this.remove.emit();
  }

}
