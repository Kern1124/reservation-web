import { FormControl, FormGroupDirective, NgForm } from "@angular/forms";
import { ErrorStateMatcher } from "@angular/material/core";

export class WhenDirtyStateMatcher implements ErrorStateMatcher{
    isErrorState(control: FormControl | null, form: FormGroupDirective | NgForm | null ): boolean {
        return control?.dirty && (control.errors?.['required'] || control.errors?.['minlength'] || control.errors?.['mail']) ||
         form?.submitted && (control?.errors?.['required'] || control?.errors?.['minlength'] || control?.errors?.['mail'])
    }
}