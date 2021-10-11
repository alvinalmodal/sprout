import React from 'react';

export function displayFieldError(errorMessages, key) {
    if (errorMessages.length > 0) {
        var filteredErrors = errorMessages.filter(error => error.key.toLowerCase() === key.toLowerCase());
        return <p className="text-danger" key={`field_error_${key}`}>{filteredErrors.map(error => error.errorMessage).join('\n')}</p>;
    }
}

export function displayUnhandledErrors(errorMessages, state) {
    if (errorMessages.length > 0) {
        var keys = Object.keys(state).map(key => key.toLowerCase());
        var unhandledErrors = errorMessages.filter(error => keys.indexOf(error.key.toLowerCase()) === -1);
        if (unhandledErrors.length > 0) {
            var errors = unhandledErrors.map( (index,error) => {
                return <p className="text-danger" key={`unhandledError_${index}`}>{unhandledErrors.map(error => error.errorMessage).join("\n")}</p>;
            });
            return errors;
        }
    }
}
