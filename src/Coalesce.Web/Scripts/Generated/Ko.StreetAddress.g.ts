
/// <reference path="../coalesce.dependencies.d.ts" />

module ViewModels {
    // *** External Type StreetAddress
    export class StreetAddress
    {

        // Observables
        public address: KnockoutObservable<string | null> = ko.observable(null);
        public city: KnockoutObservable<string | null> = ko.observable(null);
        public state: KnockoutObservable<string | null> = ko.observable(null);
        public postalCode: KnockoutObservable<string | null> = ko.observable(null);
        // Loads this object from a data transfer object received from the server.
        public parent: any;
        public parentCollection: any;

        public loadFromDto = (data: any) => {
            if (!data) return;

            // Load the properties.
            this.address(data.address);
            this.city(data.city);
            this.state(data.state);
            this.postalCode(data.postalCode);

        };

                /** Saves this object into a data transfer object to send to the server. */
        public saveToDto = (): any => {
            var dto: any = {};
            
            dto.address = this.address();
            dto.city = this.city();
            dto.state = this.state();
            dto.postalCode = this.postalCode();
            
            return dto;
        }


        constructor(newItem?: any, parent?: any){
            this.parent = parent;
            // Load the object

            if (newItem) {
                this.loadFromDto(newItem);
            }
        }
    }
}