describe('Display Matches', function () {

    // Server must be running 
    // Creates a profile, generates matches and displays matches 
    it('Creates Activity Profile and displays matches', function () {

        cy.visit('https://localhost:5002')

        cy.contains('Test').click()

        cy.contains('Matching').click()

        // Creating activity profile
        cy.contains('Activity Profile').click()

        cy.get('input[type="checkbox"]')
            .check(['Studying', 'Exercising']);
     
        cy.get('input[type="radio"]')
            .check(['one']);

        // Save and generate matches 
        cy.contains("Save").click()

        cy.contains("Generate Activity Matches").click()

        cy.contains('Display Matches').click()

        cy.contains('Return').click()

    })

    // Checks button works 
    it('Return to Matching Home', function () {
        cy.visit('https://localhost:5002/displaymatches')


        cy.contains('Return').click()


    })
})
