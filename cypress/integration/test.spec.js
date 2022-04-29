

describe('My First Test', function () {
    it('Finds an element', function () {
        cy.visit('http://studentmultitool.me/bookselling')

        cy.contains('Book')

        // checking by values
        cy.get('input[type="checkbox"]')
            .check(['Studying', 'Exercising']);
        // unchecking all values
        cy.get('input[type="checkbox"]').uncheck();
        // checking and assertion combined with and()
        cy.get('input[value="Go off campus"]')
            .check().should('be.checked').and('have.value', 'Go off campus');
        // unchecking and assertion combined with and()
        cy.get('input[value="Go off campus"]')
            .uncheck().should('not.be.checked');

    })

    it('Clicks an element', function () {
        cy.visit('http://studentmultitool.me/bookselling')

        cy.contains('save').click()


    })
})