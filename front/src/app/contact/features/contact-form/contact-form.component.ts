import { Component, signal } from '@angular/core';
import { PanelModule } from 'primeng/panel';
import { InputIconModule } from 'primeng/inputicon';
import { InputTextModule } from 'primeng/inputtext';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ButtonModule } from 'primeng/button';
import { FloatLabelModule } from 'primeng/floatlabel';
import { InputTextareaModule } from 'primeng/inputtextarea';
@Component({
  selector: 'app-contact-form',
  standalone: true,
  imports: [PanelModule,InputIconModule,InputTextModule, ReactiveFormsModule, ButtonModule,FloatLabelModule,InputTextareaModule],
  templateUrl: './contact-form.component.html',
  styleUrl: './contact-form.component.css'
})
export class ContactFormComponent {

  public readonly contactForm: FormGroup;
  public readonly messageLength = 300;
  public readonly isMessageSent = signal<boolean>(false);
  constructor(private fb: FormBuilder) {
    this.contactForm = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
      message: ['', [Validators.required, Validators.maxLength(this.messageLength)]],
    });
  }

  onFormSubmit()
  {
    this.isMessageSent.set(true);
  }
}
