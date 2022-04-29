

describe('TutoringProfile', function () {
    it('Finds an element', function () {
        cy.visit('https://localhost:5002')

        cy.contains('tutoring')

        // checking by values
        cy.get('input[type="radio"]')
            .check(['Group']);
        // unchecking all values
        cy.get('input[type="radio"]').uncheck();
        // checking and assertion combined with and()
        cy.get('input[value="individual"]')
            .check().should('be.checked').and('have.value', 'individual');
        cy.get('input[value')

    })

    it('Clicks an element', function () {
        cy.visit('https://localhost:5002/tutoringprofile')

        cy.contains('save').click()


    })
})
