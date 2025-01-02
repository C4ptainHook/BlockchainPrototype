// Get form and button elements
const walletForm = document.querySelector('.wallet-form');
const createWalletButton = document.getElementById('send-wallet');
const getBalanceButton = document.getElementById('get-wallet');

// Add event listener for wallet creation
createWalletButton.addEventListener('click', async (e) => {
    e.preventDefault();

    const nameInput = document.getElementById('name');
    const walletName = nameInput.value.trim();

    if (!walletName) {
        alert('Please enter a wallet name');
        return;
    }

    try {
        const response = await fetch('http://localhost:5000/api/v1.0/wallet/new', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify({
                nickName: walletName,
                balance: 0
            })
        });

        if (!response.ok) {
            throw new Error(`HTTP error! status: ${response.status}`);
        }

        // Clear the input field
        nameInput.value = '';
        
        // Show success message
        alert('Wallet created successfully!');

    } catch (error) {
        console.error('Error creating wallet:', error);
        alert('Failed to create wallet. Please try again.');
    }
});

// Add event listener for getting balance
getBalanceButton.addEventListener('click', async (e) => {
    e.preventDefault();

    const nameInput = document.getElementById('name');
    const walletName = nameInput.value.trim();

    if (!walletName) {
        alert('Please enter a wallet name');
        return;
    }

    try {
        const response = await fetch(`http://localhost:5000/api/v1.0/wallet/balance?name=${encodeURIComponent(walletName)}`, {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json',
            }
        });

        if (!response.ok) {
            throw new Error(`HTTP error! status: ${response.status}`);
        }

        const balance = await response.json();
        console.log('Balance:', balance);
        // Update the balance label
        const balanceLabel = document.getElementById('balance');
        balanceLabel.textContent = `Balance: ${balance}`;

    } catch (error) {
        console.error('Error fetching balance:', error);
        alert('Failed to get wallet balance. Please try again.');
    }
});