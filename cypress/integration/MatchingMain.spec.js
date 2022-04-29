// MatchingMain.spec.js created with Cypress
//
// Start writing your Cypress tests below!
// If you're unfamiliar with how Cypress works,
// check out the link below and learn how to write your first test:
// https://on.cypress.io/writing-first-test

describe('MatchingMain', function () {
    it('Finds an element', function () {
        cy.visit('https://localhost:5002')

        //cy.contains('tutoring')

        //// checking by values
        //cy.get('input[type="radio"]')
        //    .check(['Group']);
        //// unchecking all values
        //cy.get('input[type="radio"]').uncheck();
        //// checking and assertion combined with and()
        //cy.get('input[value="individual"]')
        //    .check().should('be.checked').and('have.value', 'individual');
        //cy.get('input[value')

    })

    it('Clicks an element', function () {
        cy.visit('https://localhost:5002/matchingmain')

        cy.contains('Activity Profile').click()

        cy.contains('Tutorring Profile').click()

        cy.contains('YOU ARE OPTED INTO MATCHING').click()


    })
})
