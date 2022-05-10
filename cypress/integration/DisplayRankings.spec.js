describe('Display Rankings', function () {

    // Server must be running 
    // Creates a profile, generates matches and displays matches 
    it('Displays Rankings for class', function () {

        cy.visit('https://localhost:5002')

        cy.contains('Test').click()

        cy.contains('GPA/Grade Calculator').click()

        // Creating activity profile
        cy.contains('Display Rankings').click()

        cy.get('input[id="course"]').type('CECS 491B')
        cy.get('input[id="section"]').type('5')

        cy.contains('Generate Rankings').click()

        cy.contains('Return').click()

    })

})
