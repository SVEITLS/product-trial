import { computed, Injectable, signal } from '@angular/core';
import { Product } from '../../products/data-access/product.model';

@Injectable({
  providedIn: 'root'
})
export class BasketService {

  constructor() { }

  public readonly basket = signal<Product[]>([]);
  public basketItemsCount = computed(() => this.basket().reduce((total, product) => total + product.quantity, 0));
  public basketTotal = computed(() =>
    this.basket().reduce((total, product) => total + (product.price * product.quantity), 0)
  );
  /**
 * Adds a product to the basket.
 * If the product is already in the basket, increments its quantity by 1.
 * If it's not present, adds it with a quantity of 1.
 */
  public add(product : Product)
  {
    let basket = this.basket();
    const productIndex = basket.findIndex(pro => pro.id === product.id);
    //product is not in basket yet
    if(productIndex === -1)
    {
      product.quantity = 1;
      this.basket.set([...basket, product]);
    }
    else
    {
      let productToBuyMore = basket[productIndex];
      productToBuyMore.quantity++;
      this.basket.set([...basket])
    }
  }

  public remove(product : Product)
  {
    let basket = this.basket();
    const productIndex = basket.findIndex(pro => pro.id === product.id);

    if (productIndex === -1) {
      return;
    }

    let updatedBasket = [...basket];
    updatedBasket.splice(productIndex, 1);
    this.basket.set(updatedBasket);  
  }

}
